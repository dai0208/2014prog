using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixVector;

namespace _2ClassSVMUtil
{
    public class SVMManager
    {
        static public Vector ArrayConverter(double[] array)
        {
            Vector vector = new Vector(array);
            return vector;
        }
        static public Vector ArrayConverter(List<double> ArrayList)
        {
            Vector vector = new Vector(ArrayList.ToArray());
            return vector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tdarray">double[data_num][data]</param>
        /// <returns></returns>
        static public Vector[] tdArrayConverter(double[][] tdarray)
        {
            Vector[] arrvector = new Vector[tdarray.Length];
            for (int col = 0; col < tdarray.Length; col++)
                arrvector[col] = ArrayConverter(tdarray[col]);
            return arrvector;
        }

        static public Vector[] tdArrayConverter(List<List<double>> tdArrayList)
        {
            Vector[] arrvector = new Vector[tdArrayList.Count];
            for (int col = 0; col < tdArrayList.Count; col++)
                arrvector[col] = ArrayConverter(tdArrayList[col]);
            return arrvector;
        }

        static public Matrix MatrixConverter(Vector[] vectors)
        {
            Matrix mtx = new Matrix(vectors);
            return mtx;
        }

        static public Matrix MatrixConverter(double[][] tdarray)
        {
            Matrix mtx = new Matrix(tdArrayConverter(tdarray));
            return mtx;
        }

        static public Matrix MatrixConverter(List<List<double>> tdArrayList)
        {
            Matrix mtx = new Matrix(tdArrayConverter(tdArrayList));
            return mtx;
        }
    }
}
