using System;
using System.IO;

namespace PointFormat
{
	/// <summary>
	/// �t�@�C�����o�͂��Ǘ������Ԋ�{�ƂȂ�N���X�ł��B���̃N���X�͕K���h�������Ďg��Ȃ���΂Ȃ�܂���B
	/// </summary>
	abstract public class cOpenFile
	{
		protected string strOpenFileName;
		protected DirectoryInfo diOpenFile;
		protected FileInfo fiOpenFile;
		protected System.Windows.Forms.ToolStripProgressBar pgbMain;

		/// <summary>
		/// ���ɉ������Ȃ��R���X�g���N�^�ł��B
		/// </summary>
		public cOpenFile()
		{
		}

		/// <summary>
		/// �t�@�C���̓��o�͏󋵂��������߂̃v���O���X�o�[�̒l��ݒ�A�擾���邽�߂̃v���p�e�B�ł��B�ł������w�肵�Ă��������B
		/// </summary>
        public System.Windows.Forms.ToolStripProgressBar pgbProgressBar
		{
			set
			{
				pgbMain = value;	
			}
			get
			{
				return pgbMain;
			}
		}
	}
}
