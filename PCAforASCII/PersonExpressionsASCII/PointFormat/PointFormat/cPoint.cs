using System;

namespace PointFormat
{
    /// <summary>
    /// �O�����̓_�f�[�^�̍��W��F��ێ����A�ȒP�ȃx�N�g��������������̂ł���N���X�ł��B
    /// </summary>
    [Serializable]
    public class cPoint : XYZPoint, IComparable
    {
        protected int _R, _G, _B;


        #region �v���p�e�B
        /// <summary>
        /// R�̒l���擾�A�ݒ肵�܂��B�l��0�`255�͈̔͂ɂȂ�܂��B
        /// </summary>
        public int R
        {
            get { return _R; }
            set
            {
                /*
                if (value > 255)
                    _R = 255;
                else if (value < 0)
                    _R = 0;
                else
                 */
                _R = value;
                 
            }
        }

        /// <summary>
        /// G�̒l���擾�A�ݒ肵�܂��B�l��0�`255�͈̔͂ɂȂ�܂��B
        /// </summary>
        public int G
        {
            get { return _G; }
            set
            {
                /*
                if (value > 255)
                    _G = 255;
                else if (value < 0)
                    _G = 0;
                else
                 */ 
                    _G = value;
            }
        }

        /// <summary>
        /// B�̒l���擾�A�ݒ肵�܂��B�l��0�`255�͈̔͂ɂȂ�܂��B
        /// </summary>
        public int B
        {
            get { return _B; }
            set
            {
                /*
                if (value > 255)
                    _B = 255;
                else if (value < 0)
                    _B = 0;
                else
                 */ 
                    _B = value;
            }
        }

        /// <summary>
        /// �_�̐F���擾�A�ݒ肵�܂��B
        /// </summary>
        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(_R, _G, _B); }
            set
            {
                _R = value.R;
                _G = value.G;
                _B = value.B;
            }
        }

        #endregion

        #region �R���X�g���N�^

        public cPoint()
            : base()
        {
            _R = 0;
            _G = 0;
            _B = 0;
        }

        public cPoint(double X, double Y, double Z, int R, int G, int B)
        {
            //�ϐ��ɍ��W���Z�b�g
            _X = X;
            _Y = Y;
            _Z = Z;

            //�F�������ɃZ�b�g�B
            _R = R;
            _G = G;
            _B = B;
        }

        /// <summary>
        /// �R�s�[�R���X�g���N�^
        /// </summary>
        /// <param name="icPoint">�R�s�[���ꂽ�C���X�^���X</param>
        public cPoint(cPoint icPoint)
        {
            this._X = icPoint._X;
            this._Y = icPoint._Y;
            this._Z = icPoint._Z;

            this._R = icPoint._R;
            this._G = icPoint._G;
            this._B = icPoint._B;

            this._Tag = icPoint._Tag;
        }

        public cPoint(XYZPoint XYZPoint)
        {
            this._X = XYZPoint.X;
            this._Y = XYZPoint.Y;
            this._Z = XYZPoint.Z;

            this._R = 0;
            this._G = 0;
            this._B = 0;

            this._Tag = XYZPoint.Tag;
        }

        #endregion

        //���݂̕ێ����Ă�������^�u��؂�̃f�[�^�Ƃ���string�^�ŕԂ��܂��B
        //�Ԃ��f�[�^�̃t�H�[�}�b�g�� dX dY dZ iR iG iB�̏��Ԃł��̂܂�RapidForm�œǂ߂�`�ł��B
        public override string strOutput()
        {
            return _X.ToString() + "\t" + _Y.ToString() + "\t" + _Z.ToString() + "\t" + _R.ToString() + "\t" + _G.ToString() + "\t" + _B.ToString() + "\n";
        }

        public override string ToString()
        {
            return string.Format("{0,3:F3}, {1,3:F3}, {2,3:F3}, {3,3:D}, {4,3:D}, {5,3:D}", _X, _Y, _Z, _R, _B, _G);
        }

    }

}
