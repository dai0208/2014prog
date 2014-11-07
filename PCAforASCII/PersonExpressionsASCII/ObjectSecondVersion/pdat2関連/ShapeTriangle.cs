using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointFormat;
using MatrixVector;

namespace ObjectSecondVersion
{
    public delegate int[] GetResampledIndexMethod(int Weight);

    [Serializable]
    public class ShapeTriangle:TriangleData
    {
        /// <summary>
        /// ShapeTriangleが参照する形状データ
        /// </summary>
        static cPointData _RefShapePointData;

        /// <summary>
        /// 再サンプリング前のデータインデックス
        /// </summary>
        int[] _BeforeShapeDataIndex;

        /// <summary>
        /// 再サンプリング後のデータインデックス
        /// </summary>
        int[] _AfterShapeDataIndex;

        /// <summary>
        /// 再現指数のベクトル
        /// </summary>
        Vector _RecallIndex;

        /// <summary>
        /// 再サンプリングを行うデリゲート
        /// </summary>
        GetResampledIndexMethod _ResampledMethod;

        /// <summary>
        /// 再現指数、再サンプリングが行われたかのフラグ(Trueなら計算済み)
        /// </summary>
        bool _CalclatedFlag = false;


        #region コンストラクタ

        public ShapeTriangle()
            : base()
        { this._ResampledMethod = DoResampleMethod; }

        public ShapeTriangle(TriangleData TriangleData)
            : base(TriangleData)
        { 
            this.CreateBeforeShapeDataIndex();
            this._ResampledMethod = DoResampleMethod;
        }

        public ShapeTriangle(ShapeTriangle ShapeTriangleData)
            : base((TriangleData)ShapeTriangleData)
        {
            this._AfterShapeDataIndex = (int[])ShapeTriangleData._AfterShapeDataIndex.Clone();
            this._BeforeShapeDataIndex = (int[])ShapeTriangleData._BeforeShapeDataIndex.Clone();
            this._ResampledMethod = DoResampleMethod;
            this._RecallIndex = new Vector(ShapeTriangleData._RecallIndex);
        }

        public ShapeTriangle(XYZPoint[] Point)
        {
            this.Triangles = Point;
            this.CreateBeforeShapeDataIndex();
            this._ResampledMethod = DoResampleMethod;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// ShapeTriangleが参照する形状データを設定します
        /// </summary>
        /// <param name="RefShapePointData">参照する形状データ</param>
        public static void SetRefShapePointData(cPointData RefShapePointData)
        {
            _RefShapePointData = RefShapePointData;
        }

        /// <summary>
        /// 「この三角形が保持する再サンプリング前データ」に対応するインデックスを設定します
        /// </summary>
        /// <param name="ShapeIndex">点群データのインデックス</param>
        public virtual void CreateBeforeShapeDataIndex()
        {
            if (_RefShapePointData != null)
                _BeforeShapeDataIndex = this.GetIndexInTriangle(_RefShapePointData, 10d);
            else
                throw new ApplicationException("先にShapeTriangle.SetRefShapePointDataで参照元形状を指定してください。");

            CalclatedFlag = false;
        }

         /// <summary>
        /// 三角形の頂点座標を行列形式で取得します
        /// </summary>
        /// <returns></returns>
        public Matrix GetMatrix()
        {
            ColumnVector[] Vectors = new ColumnVector[3];
            {
                double[] Element = new double[3];
                Element[0] = Point1.X;
                Element[1] = Point1.Y;
                Element[2] = Point1.Z;
                Vectors[0] = new ColumnVector(Element);
            }
            {
                double[] Element = new double[3];
                Element[0] = Point2.X;
                Element[1] = Point2.Y;
                Element[2] = Point2.Z;
                Vectors[1] = new ColumnVector(Element);
            }
            {
                double[] Element = new double[3];
                Element[0] = Point3.X;
                Element[1] = Point3.Y;
                Element[2] = Point3.Z;
                Vectors[2] = new ColumnVector(Element);
            }
            return new Matrix(Vectors);
        }
       
        /// <summary>
        /// 現在の三角形に対して再サンプリングを行います。
        /// </summary>
        /// <returns></returns>
        protected virtual int[] DoResampleMethod(int Weight)
        {
            XYZPointData RefTrianglePointData;
            int[] ResultIndexData;
            #region 基準三角形の作成＆移動
            {
                {
                    int WightSum = ((Weight * Weight) + Weight) / 2;
                    XYZPoint[] RefTrianglePoint = new XYZPoint[WightSum];
                    int Count = 0;

                    for (int i = 0; i < Weight; i++)
                        for (int j = 0; j < Weight - i; j++)
                            RefTrianglePoint[Count++] = new XYZPoint((double)i / Weight, (double)j / Weight, 0);

                    RefTrianglePointData = new XYZPointData(RefTrianglePoint);
                }
                //RefTrianglePointData = MoveOnBaseTriangle.GetDataInverseOrderOnBase(RefTrianglePointData);
            }
            #endregion

            {
                ResultIndexData = new int[RefTrianglePointData.Length];
                XYZPointData BeforePointData = new XYZPointData(_RefShapePointData,_BeforeShapeDataIndex);
                MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(this);
                BeforePointData = MoveOnBaseTriangle.GetDataInOrderOnBase(BeforePointData);

                for (int i = 0; i < RefTrianglePointData.Length; i++)
                {
                    double MinDistance = double.MaxValue;

                    for (int j = 0; j < BeforePointData.Length; j++)
                    {
                        double Distance = RefTrianglePointData[i].Distance(BeforePointData[j].X, BeforePointData[j].Y);
                        if(MinDistance > Distance)
                        {
                            MinDistance = Distance;
                            ResultIndexData[i] = _BeforeShapeDataIndex[j];
                        }
                    }
                }

            }

            return ResultIndexData;
        }

        protected virtual void DoResample()
        {
            _AfterShapeDataIndex = _ResampledMethod(this.Weight);
        }

        protected virtual void CalcRecallIndex()
        {
            _RecallIndex = ErrorIndex.cCalcIndex.GetRecallIndex(this.BeforeShapeData, this.AfterShapeData);
        }

        public virtual void CalcAnything()
        {
            CalclatedFlag = true;
            this.DoResample();
            this.CalcRecallIndex();
        }

        public virtual RecipeValue GetBestRecipe()
        {
            return new RecipeValue(0.33, 0.33);
            //TODO 今後GetBestRecipeを実装すること
            #region BestRecipeを探索する
            double OldRecallIndexAverage = this.RecallIndex.GetAverage();
            double NewRecallIndexAverage;
            double CenterRecallIndex;

            Random rnd = new Random();
            double Alpha; 
            double Beta;
            do
            {
                Alpha = rnd.Next(11) / 10d;
                Beta = rnd.Next(11) / 10d;
                Console.WriteLine("{0} : {1}",Alpha,Beta);
            }
            while (((Alpha == 0) & (Beta == 0)) | ((Alpha == 0) & (Beta == 1)) | ((Alpha == 1) & (Beta == 0)) || (Alpha + Beta > 1));
            Console.WriteLine("");
            NewRecallIndexAverage = this.GetRecallIndex(new RecipeValue(Alpha, Beta));
            CenterRecallIndex = this.GetRecallIndex(new RecipeValue(0.33, 0.33));

            double MaxAverage = new double[]{OldRecallIndexAverage, NewRecallIndexAverage, CenterRecallIndex}.Min();

            if (MaxAverage == NewRecallIndexAverage)
                return new RecipeValue(Alpha, Beta);
            else if (MaxAverage == CenterRecallIndex)
                return new RecipeValue(0.33, 0.33);
            else
                return new RecipeValue(0, 0);
            #endregion
        }

        protected virtual double GetRecallIndex(RecipeValue RecipeValue)
        {
            ShapeTriangle[] NewShapeTriangle;

            XYZPoint RefPoint = new XYZPoint(RecipeValue.Alpha, RecipeValue.Beta, 0);
            int NewPointIndex = PointFormat.SearchNearestPoint.Get2DNearestPointIndexFastOnBase(new XYZPointData(this.BeforeShapeData), RefPoint, this, 10d);
            XYZPoint NewFeturePoint = (XYZPoint)BeforeShapeData[NewPointIndex];

            switch (RecipeValue.Kind)
            {
                case KindOfRecipe.Inner:
                    NewShapeTriangle = new ShapeTriangle[3];
                    NewShapeTriangle[0] = new ShapeTriangle(new XYZPoint[] { Point1, Point2, NewFeturePoint });
                    NewShapeTriangle[1] = new ShapeTriangle(new XYZPoint[] { Point2, Point3, NewFeturePoint });
                    NewShapeTriangle[2] = new ShapeTriangle(new XYZPoint[] { Point3, Point1, NewFeturePoint });
                    break;
                case KindOfRecipe.OnSide1_2:
                    NewShapeTriangle = new ShapeTriangle[2];
                    NewShapeTriangle[0] = new ShapeTriangle(new XYZPoint[] { Point1, Point3, NewFeturePoint });
                    NewShapeTriangle[1] = new ShapeTriangle(new XYZPoint[] { Point2, Point3, NewFeturePoint });
                    break;
                case KindOfRecipe.OnSide2_3:
                    NewShapeTriangle = new ShapeTriangle[2];
                    NewShapeTriangle[0] = new ShapeTriangle(new XYZPoint[] { Point1, Point2, NewFeturePoint });
                    NewShapeTriangle[1] = new ShapeTriangle(new XYZPoint[] { Point3, Point1, NewFeturePoint });
                    break;
                case KindOfRecipe.OnSide3_1:
                    NewShapeTriangle = new ShapeTriangle[2];
                    NewShapeTriangle[0] = new ShapeTriangle(new XYZPoint[] { Point1, Point2, NewFeturePoint });
                    NewShapeTriangle[1] = new ShapeTriangle(new XYZPoint[] { Point2, Point3, NewFeturePoint });
                    break;
                default:
                    throw new ApplicationException("それはおかしいです。");
            }

            double ResultVector = 0;
            for (int i = 0; i < NewShapeTriangle.Length; i++)
            {
                NewShapeTriangle[i].CalcAnything();
                ResultVector += NewShapeTriangle[i].RecallIndex.GetAverage();
            }

            return ResultVector / NewShapeTriangle.Length;
        }
        #endregion

        #region プロパティ
         /// <summary>
        /// 再サンプリング前の点群データを取得します
        /// </summary>
        public cPointData BeforeShapeData
        {
            get {
                if (_RefShapePointData == null)
                    throw new ApplicationException("参照元の点群データがありません。");
                if (_BeforeShapeDataIndex.Length == 0)
                    throw new ApplicationException("三角形内に点がありません");

                cPointData ResultPointData = _RefShapePointData.GetIndexData(_BeforeShapeDataIndex);

                if (ResultPointData == null)
                    throw new ApplicationException("再サンプリング前の点群データがありません。");
                return ResultPointData; 
            }
        }

        /// <summary>
        /// 再サンプリング後の点群データを取得します
        /// </summary>
        public cPointData AfterShapeData
        {
            get {
                if (_RefShapePointData == null)
                    throw new ApplicationException("参照元の点群データがありません。");
                if (_BeforeShapeDataIndex.Length == 0)
                    throw new ApplicationException("三角形内に点がありません");

                if (CalclatedFlag == false)
                    this.CalcAnything();

                cPointData ResultPointData = _RefShapePointData.GetIndexData(_AfterShapeDataIndex);

                if (ResultPointData == null)
                    throw new ApplicationException("再サンプリング後の点群データがありません。");
                return ResultPointData;
            }
        }

        /// <summary>
        /// 頂点１から頂点２へのベクトルを取得します
        /// </summary>
        public Vector Vector12
        {
            get
            {
                double[] Element = new double[3];
                Element[0] = Point2.X - Point1.X;
                Element[1] = Point2.Y - Point1.Y;
                Element[2] = Point2.Z - Point1.Z;

                return new Vector(Element);
            }
        }

        /// <summary>
        /// 頂点１から頂点３へのベクトルを取得します
        /// </summary>
        public Vector Vector13
        {
            get
            {
                double[] Element = new double[3];
                Element[0] = Point3.X - Point1.X;
                Element[1] = Point3.Y - Point1.Y;
                Element[2] = Point3.Z - Point1.Z;

                return new Vector(Element);
            }
        }

        /// <summary>
        /// 頂点1を取得、設定します。新しい頂点を設定すると内部に含まれる点を自動的に計算するため、
        /// 時間がかかります。3点とも設定するときはTrianglesで一度に設定することをお勧めします。
        /// </summary>
        public override XYZPoint Point1
        {
            get { return base.Point1; }
            set
            {
                base.Point1 = value;
                this.CreateBeforeShapeDataIndex();
            }
        }

        /// <summary>
        /// 頂点2を取得、設定します。新しい頂点を設定すると内部に含まれる点を自動的に計算するため、
        /// 時間がかかります。3点とも設定するときはTrianglesで一度に設定することをお勧めします。
        /// </summary>
        public override XYZPoint Point2
        {
            get { return base.Point2; }
            set
            {
                base.Point2 = value;
                this.CreateBeforeShapeDataIndex();
            }
        }

        /// <summary>
        /// 頂点3を取得、設定します。新しい頂点を設定すると内部に含まれる点を自動的に計算するため、
        /// 時間がかかります。3点とも設定するときはTrianglesで一度に設定することをお勧めします。
        /// </summary>
        public override XYZPoint Point3
        {
            get { return base.Point3; }
            set
            {
                base.Point3 = value;
                this.CreateBeforeShapeDataIndex();
            }
        }

        public override XYZPoint this[int Index]
        {
            get { return base[Index]; }
            set
            {
                base[Index] = value;
                this.CreateBeforeShapeDataIndex();
            }
        }

        public override XYZPoint[] Triangles
        {
            get { return base.Triangles; }
            set
            {
                base.Triangles = value;
                this.CreateBeforeShapeDataIndex();
            }
        }

        public static cPointData RefShapePointData
        {
            get { return _RefShapePointData; }
            set { _RefShapePointData = new cPointData(value); }
        }

        public Vector RecallIndex
        {
            get {
                if (_CalclatedFlag == false)
                    this.CalcAnything();

                return _RecallIndex;
            }
        }

        public bool CalclatedFlag
        {
            get { return _CalclatedFlag; }
            private set { _CalclatedFlag = value; }
        }

        public override int Weight
        {
            get
            {
                return base.Weight;
            }
            set
            {
                base.Weight = value;
                CalclatedFlag = false;
            }
        }
        #endregion
        
        public override bool Equals(object obj)
        {
            ShapeTriangle RefShapeTriangle = (ShapeTriangle)obj;
            if (RefShapeTriangle.Point1 == this.Point1 & RefShapeTriangle.Point2 == this.Point2 & RefShapeTriangle.Point3 == this.Point3)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
