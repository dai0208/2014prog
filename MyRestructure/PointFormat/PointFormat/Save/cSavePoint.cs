using System;
using System.IO;
using System.Collections;

namespace PointFormat
{
	
	// <summary>
	/// �|�C���g�f�[�^���������ނ��߂̃N���X�ł��B
	/// </summary>
	public class cSavePoint : cSaveFile
	{
		private cPoint[] ipdPointData;

		/// <summary>
		/// �t�@�C�������w�肵�ăf�[�^���������ނ��߂̃��\�b�h�ł��B
		/// </summary>
		/// <param name="strSaveFileName">�������ݐ�̃t�@�C����</param>
		/// <param name="ipdData">�������ރ|�C���g�f�[�^�z��</param>
		public cSavePoint(string strSaveFileName, cPoint[] ipdData)
		{
			base.strOpenFileName = strSaveFileName;
			this.ipdPointData = ipdData;
			base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
			base.fiOpenFile = new FileInfo(base.strOpenFileName);  
		}

        public cSavePoint()
        {
        }

        public void vSetSavePoint(string strSaveFileName, cPoint[] ipdData)
        {
            base.strOpenFileName = strSaveFileName;
            this.ipdPointData = ipdData;
            base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
            base.fiOpenFile = new FileInfo(base.strOpenFileName);
        }

        public void vSetSavePoint(string strSaveFileName, ArrayList alData)
        {
            base.strOpenFileName = strSaveFileName;
            this.ipdPointData = new cPoint[alData.Count];
            for (int i = 0; i < alData.Count; i++)
                ipdPointData[i] = new cPoint((cPoint)alData[i]);
            base.diOpenFile = new DirectoryInfo(Path.GetDirectoryName(base.strOpenFileName));
            base.fiOpenFile = new FileInfo(base.strOpenFileName);
        }


		/// <summary>
		/// �t�@�C���Ƀf�[�^�����ۂɏ������ނ��߂̃��\�b�h�ł��B
		/// </summary>
		/// <returns>true�Ȃ珑�����ݐ����Bfalse�Ȃ珑�����ݎ��s�ł��B</returns>
		protected override bool bWriteFile()
		{
			//�ۑ����镨�̂����邩�ǂ����B
			if(this.ipdPointData == null)
				return false;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < this.ipdPointData.Length; i++)
                sb.Append(this.ipdPointData[i].strOutput());

            try
            {
                swSaveFile.Write(sb.ToString());
                /*
                for(int i = 0; i < this.ipdPointData.Length ; i++)
                {
                    swSaveFile.WriteLine(this.ipdPointData[i].strOutput());

                    //�i�s�󋵂�\���v���O���X�o�[�̒l�𑝉��B�����̈Ӗ����킩��Ȃ��Ă��e���Ȃ��ł��B
                    if(pgbMain != null)
                        if(this.ipdPointData.Length >= 100)
                            if((i % ((int)(this.ipdPointData.Length / 100) * 5)) == 1)
                                base.pgbMain.PerformStep();
                        else
                            base.pgbMain.Value = 100;
                }
                */
            }
            catch { return false; }
			finally
			{
				if(swSaveFile != null)
					swSaveFile.Close();
			}

			return true;
		}
	}
}
