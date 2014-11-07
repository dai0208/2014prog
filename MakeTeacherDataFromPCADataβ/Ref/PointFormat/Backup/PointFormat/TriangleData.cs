using System;
using System.Collections.Generic;

namespace PointFormat
{


    /// <summary>
    /// �O�p�`�̊e���_�̍��W��ێ�����\����
    /// </summary>
    [Serializable]
    public class TriangleData
    {
        #region �v���C�x�[�g�ϐ�
        /// <summary>
        /// ���_�P
        /// </summary>
        private XYZPoint _Point1;

        /// <summary>
        /// ���_�Q
        /// </summary>
        private XYZPoint _Point2;

        /// <summary>
        /// ���_�R
        /// </summary>
        private XYZPoint _Point3;
        
        /// <summary>
        /// �O�p�`�̏d��
        /// </summary>
        private int _Weight;

        #endregion

        #region �R���X�g���N�^

        public TriangleData()
        {
            this._Point1 = new XYZPoint(0, 0, 0);
            this._Point2 = new XYZPoint(0, 0, 0);
            this._Point3 = new XYZPoint(0, 0, 0);
            this.Weight = 30;
        }

        public TriangleData(TriangleData TriangleData)
        {
            this._Point1 = new XYZPoint(TriangleData.Point1);
            this._Point2 = new XYZPoint(TriangleData.Point2);
            this._Point3 = new XYZPoint(TriangleData.Point3);
            this._Weight = TriangleData.Weight;
        }
        #endregion

        /// <summary>
        /// �O�p�`�̏d�S�̍��W���v�Z���܂�
        /// </summary>
        /// <returns>�d�S�̍��W</returns>
        public XYZPoint GetCenterPoint()
        {
            XYZPoint cp = new XYZPoint();

            cp.X = (_Point1.X + _Point2.X + _Point3.X) / 3d;
            cp.Y = (_Point1.Y + _Point2.Y + _Point3.Y) / 3d;
            cp.Z = (_Point1.Z + _Point2.Z + _Point3.Z) / 3d;

            return cp;
        }

        /// <summary>
        /// �O�p�`�̖ʐς��擾���܂��B
        /// </summary>
        public double Area
        {
            get
            {
                XYZPoint VectorA = new XYZPoint(_Point2.X - _Point1.X, _Point2.Y - _Point1.Y, _Point2.Z - _Point1.Z);
                XYZPoint VectorB = new XYZPoint(_Point3.X - _Point1.X, _Point3.Y - _Point1.Y, _Point3.Z - _Point1.Z);

                double NormA = Math.Sqrt(VectorA.X * VectorA.X + VectorA.Y * VectorA.Y + VectorA.Z * VectorA.Z);
                double NormB = Math.Sqrt(VectorB.X * VectorB.X + VectorB.Y * VectorB.Y + VectorB.Z * VectorB.Z);

                double InnerProduct = VectorA.X * VectorB.X + VectorA.Y * VectorB.Y + VectorA.Z * VectorB.Z;

                double Area = (1d / 2d) * Math.Sqrt(NormA * NormA * NormB * NormB - InnerProduct * InnerProduct);

                return Area;
            }
        }

        #region �O�p�`�̓���PointData�擾�n
        public int[] GetIndexInTriangle(cPointData PointData,  double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("臒l�͐��̒l�ɂ��ĉ������B");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(this);
            var MovedPointData = MoveOnBaseTriangle.GetDataInOrderOnBase(PointData);

            //�C���f�b�N�X���X�g
            List<int> IndexList = new List<int>();

            //���ׂĂ̓_���O�p�`�����ǂ�������
            for (int i = 0; i < PointData.Length; i++)
            {
                double ChangeX = MovedPointData[i].X;
                double ChangeY = MovedPointData[i].Y;
                //�O�p�`���̂Ƃ��͉��Ԗڂ�����������(�O�p�`�̎���S�����O�p�`���ƌ��Ȃ��j
                if ((ChangeX >= -0.04) & (ChangeY >= -0.04) & (ChangeX + ChangeY <= 1.04) & (MovedPointData[i].Z > -Threshould))
                    IndexList.Add(i);
            }

            return IndexList.ToArray();
        }

        public int[] GetIndexInTriangle(XYZPointData PointData, double Threshould)
        {
            if (Threshould <= 0)
                throw new ApplicationException("臒l�͐��̒l�ɂ��ĉ������B");

            MoveOnBaseTriangle MoveOnBaseTriangle = new MoveOnBaseTriangle(this);
            var MovedPointData = MoveOnBaseTriangle.GetDataInOrderOnBase(PointData);

            //�C���f�b�N�X���X�g
            List<int> IndexList = new List<int>();

            //���ׂĂ̓_���O�p�`�����ǂ�������
            for (int i = 0; i < PointData.Length; i++)
            {
                double ChangeX = MovedPointData[i].X;
                double ChangeY = MovedPointData[i].Y;
                //�O�p�`���̂Ƃ��͉��Ԗڂ�����������(�O�p�`�̎���S�����O�p�`���ƌ��Ȃ��j
                if ((ChangeX >= -0.04) & (ChangeY >= -0.04) & (ChangeX + ChangeY <= 1.04) & (MovedPointData[i].Z > -Threshould))
                    IndexList.Add(i);
            }

            return IndexList.ToArray();
        }

        public cPointData GetPointDataInTriangle(cPointData PointData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, Threshould);
            return PointData.GetIndexData(Index);
        }

        public XYZPointData GetPointDataInTriangle(XYZPointData PointData, double Threshould)
        {
            int[] Index = GetIndexInTriangle(PointData, Threshould);
            return PointData.GetIndexData(Index);
        }

        #endregion

        #region �ړ��E��]
        /// <summary>
        /// �O�p�`���ړ�������
        /// </summary>
        /// <param name="dX">X�̈ړ���</param>
        /// <param name="dY">Y�̈ړ���</param>
        /// <param name="dZ">Z�̈ړ���</param>
        public void Move(double dX, double dY, double dZ)
        {
            this.Point1.Move(dX, dY, dZ);
            this.Point2.Move(dX, dY, dZ);
            this.Point3.Move(dX, dY, dZ);
        }

        /// <summary>
        /// X���܂��ɉ�]������
        /// </summary>
        /// <param name="dTheta">��]�p�x</param>
        public void Rotate_X(double dTheta)
        {
            this.Point1.RotateR_X(dTheta);
            this.Point2.RotateR_X(dTheta);
            this.Point3.RotateR_X(dTheta);

        }

        /// <summary>
        /// Y���܂��ɉ�]������
        /// </summary>
        /// <param name="dTheta">��]�p�x</param>
        public void Rotate_Y(double dTheta)
        {
            this.Point1.RotateR_Y(dTheta);
            this.Point2.RotateR_Y(dTheta);
            this.Point3.RotateR_Y(dTheta);
        }

        /// <summary>
        /// Z���܂��ɉ�]������
        /// </summary>
        /// <param name="dTheta">��]�p�x</param>
        public void Rotate_Z(double dTheta)
        {
            this.Point1.RotateR_Z(dTheta);
            this.Point2.RotateR_Z(dTheta);
            this.Point3.RotateR_Z(dTheta);

        }
        #endregion

        #region �v���p�e�B
        /// <summary>
        /// �O�p�`���_���擾�A�ݒ肵�܂��B
        /// </summary>
        public virtual XYZPoint[] Triangles
        {
            get
            {
                XYZPoint[] icpWorkPoint = new XYZPoint[3];
                icpWorkPoint[0] = _Point1;
                icpWorkPoint[1] = _Point2;
                icpWorkPoint[2] = _Point3;

                return icpWorkPoint;
            }
            set
            {
                _Point1 = new XYZPoint(value[0]);
                _Point2 = new XYZPoint(value[1]);
                _Point3 = new XYZPoint(value[2]);
            }
        }

        /// <summary>
        /// ���_�P���擾�A�ݒ肵�܂��B
        /// </summary>
        public virtual XYZPoint Point1
        {
            get { return _Point1; }
            set { _Point1 = value; }
        }

        /// <summary>
        /// ���_�Q���擾�A�ݒ肵�܂��B
        /// </summary>
        public virtual XYZPoint Point2
        {
            get { return _Point2; }
            set { _Point2 = value; }
        }

        /// <summary>
        /// ���_�R���擾�A�ݒ肵�܂��B
        /// </summary>
        public virtual XYZPoint Point3
        {
            get { return _Point3; }
            set { _Point3 = value; }
        }

        public virtual XYZPoint this[int Index]
        {
            get { return this.Triangles[Index]; }
            set { this.Triangles[Index] = value; }
        }

        public virtual int Weight
        {
            get { return this._Weight; }
            set { this._Weight = value; }
        }

        #endregion

        public override string ToString()
        {
            return "Point1: " + this._Point1.ToString() + "   \tPoint2: " + this._Point2.ToString() + "   \tPoint3: " + this._Point3.ToString() + "\n";
        }

        public void SaveAscii(string SaveFileName)
        {
            XYZPointData xyzData = new XYZPointData();
            xyzData.Items = new XYZPoint[3];
            xyzData[0] = Point1;
            xyzData[1] = Point2;
            xyzData[2] = Point3;

            xyzData.Save(SaveFileName);
        }
    }
}