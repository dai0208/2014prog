using System;
using System.IO;
using System.Windows.Forms;

namespace PointFormat
{
	/// <summary>
	/// rapidForm�̃f�[�^��ǂݍ��ނ��߂̃N���X�ł��BcOpenFile�����N���X�ł��B
	/// </summary>
	abstract public class cLoadFile : cOpenFile
	{
		protected StreamReader srLoadFile;

		public string strFileName
		{
			get
			{
				return base.strOpenFileName;
			}
			set
			{
				base.strOpenFileName = value;
			}
		}
	

		/// <summary>
		/// �R���X�g���N�^�ł����A���ɉ������܂���B
		/// </summary>
		protected cLoadFile()
		{
		}


		/// <summary>
		/// �t�@�C���̓��e��ǂݍ��ނ��߂̃��\�b�h�ł��B�O������͂��̃��\�b�h���g���ăt�@�C����ǂݍ��݂܂��B
		/// �t�@�C���̑��݃`�F�b�N�Ȃǂ��s���Ă���̂ŁA���݂��Ȃ��t�@�C�������w�肳��Ă��Ă����v�ł��B
		/// </summary>
		/// <returns>true�Ȃ�ǂݍ��ݐ����Bfalse�Ȃ�ǂݍ��ݎ��s�ł��B</returns>
		public bool bReadData()
		{
            if (base.fiOpenFile == null)
                return false;

			//�t�@�C�������݂��邩�ǂ����̃`�F�b�N�B
            if (base.fiOpenFile.Exists != true)
            {
                MessageBox.Show("�ǂݍ��ރt�@�C�������݂��܂���B\n�t�@�C������������x�w�肵�Ă��������B", "�x��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

			//�ǂݍ��݃t�@�C���̃X�g���[���ݒ�B
			srLoadFile = new StreamReader(base.strOpenFileName);

			return bReadFile();
		}

		/// <summary>
		/// �h�������N���X�����ۂɃt�@�C���̒��g��ǂݍ��ނ��߂̃��\�b�h�ł��B
		/// �h���N���X�͕K�����̃��\�b�h���������Ȃ���΂Ȃ�܂���B
		/// </summary>
		/// <returns>true�Ȃ�ǂݍ��ݐ����Bfalse�Ȃ�ǂݍ��ݎ��s�Ƃ��č���Ă��������B</returns>
		//���ۃ��\�b�h�Ȃ̂Ōp�����ă��\�b�h��K���������Ȃ���΂����Ȃ��̂ł��B
		//���return�ŌĂ΂�Ă���̂͌p�����bReadFile()�ł��B
		protected abstract bool bReadFile();
	}
}
