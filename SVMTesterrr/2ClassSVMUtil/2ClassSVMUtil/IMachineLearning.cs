using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;

namespace _2ClassSVMUtil
{
    interface IMachineLearning
    {
        /* 入力データ */
        Vector UnknownData { set; }
        Matrix TrueTeacherDatas { set; }
        Matrix FalseTeacherDatas { set; }

        /* 出力データ */
        double ResultClass { get; }
        double[] Problem { get; }
    }
}
