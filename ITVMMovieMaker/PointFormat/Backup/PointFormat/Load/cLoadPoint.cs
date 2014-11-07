using System;
using System.IO;

namespace PointFormat
{
	/// <summary>
	///  �t�@�C������|�C���g�f�[�^��ǂݏo�����߂̃N���X�ł��B
	///  ���N���X��cLoadFile�ł��B
	/// </summary>
	public class cLoadPoint : cLoadFile
	{
		private cPoint[] ipdPointData;
		private string strLoadLines = "";
		private string[] strLoadLine;

		/// <summary>
		///�ǂݏo�����|�C���g�f�[�^���擾���邽�߂̃v���p�e�B�ł��B
		/// </summary>
        public cPoint[] ipPoint
        {
            get { return this.ipdPointData; }
        }

		/// <summary>
		/// �ǂݏo�����|�C���g�f�[�^�z��̗v�f�����擾���邽�߂̃v���p�e�B�ł��B
		/// </summary>
        public int iPointNo
        {
            get { return this.ipdPointData.Length; }
        }

		/// <summary>
		/// �R���X�g���N�^�ł��B�ǂݏo���t�@�C�������w�肵�܂��B�ǂݏo������bReadFile���\�b�h�����s���Ă��������B
		/// </summary>
		/// <param name="strLoadFileName">�ǂݏo���t�@�C����</param>
		public cLoadPoint(string strLoadFileName)
		{
            try
            {
                base.strOpenFileName = strLoadFileName;
                base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
                base.fiOpenFile = new FileInfo(base.strOpenFileName);
            }
            catch
            {
                base.strFileName = "";
                base.diOpenFile = null;
                base.fiOpenFile = null;
            }
		}

		/// <summary>
		///���̃��\�b�h�����s����ƃt�@�C������f�[�^��ǂݏo���܂��B
		/// </summary>
		/// <returns>true�Ȃ�ǂݏo�������Bfalse�Ȃ�ǂݏo�����s�ł��B</returns>
        protected override bool bReadFile()
        {
            try { strLoadLines = srLoadFile.ReadToEnd(); }
            catch { return false; }
            finally { srLoadFile.Close(); }

            //�Ō�̕s�v�ȉ��s���폜
            strLoadLines = strLoadLines.TrimEnd('\n');
            strLoadLines = strLoadLines.Replace(" ", ",");
            strLoadLines = strLoadLines.Replace("\r", "");

            //���s���ƂɃf�[�^���킯��
            strLoadLine = strLoadLines.Split('\n');
            this.ipdPointData = new cPoint[strLoadLine.Length];

            if (base.pgbMain != null)
                base.pgbMain.Value = 0;

            for (int i = 0; i < strLoadLine.Length; i++)
            {
                double dX, dY, dZ;
                int iR, iG, iB;

                strLoadLine[i] = strLoadLine[i].Replace(",", "\t");
                strLoadLine[i] = strLoadLine[i].Replace(" ", "\t");

                //X,Y,Z�f�[�^�𕪗��B
                //�f�[�^�̓^�u��؂�ɂȂ��Ă���̂Ń^�u�ŕ�����B
                dX = Double.Parse(strLoadLine[i].Split('\t')[0]);
                dY = Double.Parse(strLoadLine[i].Split('\t')[1]);
                dZ = Double.Parse(strLoadLine[i].Split('\t')[2]);

                //���l��R,G,B�̃f�[�^�𕪗��B
                if (strLoadLine[i].Split('\t').Length > 3)
                {
                    iR = Int32.Parse(strLoadLine[i].Split('\t')[3]);
                    iG = Int32.Parse(strLoadLine[i].Split('\t')[4]);
                    iB = Int32.Parse(strLoadLine[i].Split('\t')[5]);
                }
                else
                {
                    iR = 0;
                    iG = 0;
                    iB = 0;
                }
                //�f�[�^��ێ�����N���X�̔z��ɒǉ��B
                this.ipdPointData[i] = new cPoint(dX, dY, dZ, iR, iG, iB);

                //�i�s�󋵂�\���v���O���X�o�[�̒l�𑝉��B�����̈Ӗ����킩��Ȃ��Ă��e���Ȃ��ł��B
                if (pgbMain != null)
                {
                    if (strLoadLine.Length >= 100)
                        if ((i % ((int)(strLoadLine.Length / 100) * 5)) == 1)
                            base.pgbMain.PerformStep();
                        else
                            base.pgbMain.Value = base.pgbMain.Maximum;
                }
            }
            if (base.pgbMain != null)
                base.pgbMain.Value = 0;
            return true;
        }
    }
}
