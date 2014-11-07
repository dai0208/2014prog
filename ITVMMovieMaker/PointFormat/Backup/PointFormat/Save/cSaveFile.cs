using System;
using System.IO;
using System.Windows.Forms;

namespace PointFormat
{
	/// <summary>
	/// cSaveFile �̊T�v�̐����ł��B
	/// </summary>
	abstract public class cSaveFile : cOpenFile
	{
		protected StreamWriter swSaveFile;
        private bool bForceSave = false;

		/// <summary>
		/// ���ɉ������Ȃ��R���X�g���N�^�ł��B
		/// </summary>
		protected cSaveFile()
		{
		}

		//�f�[�^���Z�[�u���邽�߂̃��\�b�h�B
		/// <summary>
		/// �t�@�C���̓��e���������ނ��߂̃��\�b�h�ł��B�O������͂��̃��\�b�h���g���ăt�@�C�����������݂܂��B
		/// �t�@�C���̑��݃`�F�b�N�Ȃǂ��s���Ă���̂ŁA���݂��Ȃ��t�@�C�������w�肳��Ă��Ă������I�ɍ쐬���������݂��s���܂��B
		/// �܂��A�t�H���_�����݂��Ȃ��ꍇ�A���łɃt�@�C�������݂��Ă���ꍇ�����l�Ƀ��b�Z�[�W���o���A�����I�ɏ������s���܂��B
		/// </summary>
		/// <returns>true�Ȃ珑�����ݐ����Bfalse�Ȃ珑�����ݎ��s�B</returns>
		public bool bSaveFile()
		{
			//�ۑ�����t�H���_�����݂��邩�ǂ����B
			if(base.diOpenFile.Exists != true)
			{
				if(MessageBox.Show("�w�肳�ꂽ�t�H���_�͑��݂��܂���B\n�쐬���܂����H", "�t�H���_������܂���", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					//�t�H���_���쐬���Ȃ��炵���̂ŕۑ����s�B
					return false;
				}

				//�ۑ��f�B���N�g�����쐬
				base.diOpenFile.Create();
			}

			//���������͕K���ۑ�����t�H���_��������

            if (base.fiOpenFile.Exists == true && bForceSave == false)
            {
                if (MessageBox.Show("�w�肳�ꂽ�t�@�C���͂��łɑ��݂��܂��B\n�㏑���ۑ����܂����H", "�㏑���m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    //�㏑���ۑ��͂���炵���̂ŕۑ����s�B
                    return false;
                }
                else
                {
                    //�㏑��OK�炵���̂Ńt�@�C�����폜
                    base.fiOpenFile.Delete();
                }
            }
            else
            {
                base.fiOpenFile.Delete();
            }

			//���������͊�{�I�ɕۑ��ł���������݂����ԁB

			//�f�[�^���Z�[�u���邽�߂̃X�g���[�����쐬�B
			swSaveFile = new StreamWriter(base.strOpenFileName);

			return bWriteFile();
		}

        public bool bForceSave_value
        {
            get
            {
                return bForceSave;
            }
            set
            {
                bForceSave = value;
            }
        }

		/// <summary>
		/// �h�������N���X�����ۂɃt�@�C���̒��g���������ނ��߂̃��\�b�h�ł��B
		/// �h���N���X�͕K�����̃��\�b�h���������Ȃ���΂Ȃ�܂���B
		/// </summary>
		/// <returns></returns>
		//���ۃ��\�b�h�Ȃ̂Ōp�����ă��\�b�h��K���������Ȃ���΂����Ȃ��̂ł��B
		//���return�ŌĂ΂�Ă���̂͌p�����bWriteFile()�ł��B
		protected abstract bool bWriteFile();

	}
}
