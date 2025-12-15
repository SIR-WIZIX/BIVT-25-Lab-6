using System;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6
{
  public class Purple
  {

    public int FindDiagonalMaxIndex(int[,] matrix){
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      if (n!=m || n == 0) return -1;

      int mx = 0;
      for (int i = 0; i < n; i++){
	if (matrix[mx, mx] < matrix[i, i]){
	  mx = i;
	}
      }

      return mx;
    }

    public void SwapRowColumn(int[,] matrix, int rowIndex, int[,] B, int columnIndex){

      int n1 = matrix.GetLength(0);
      int m1 = matrix.GetLength(1);

      int n2 = B.GetLength(0);
      int m2 = B.GetLength(1);

      if (n1 != m1 || n1 != m2 || n2 != m2){
	return;
      }

      for (int i =0; i < n1; i++){
	(matrix[rowIndex, i], B[i, columnIndex]) = (B[i, columnIndex], matrix[rowIndex, i]);
      }
    }

    public void Task1(int[,] A, int[,] B)
    {

      // code here

      int n1 = A.GetLength(0);
      int m1 = A.GetLength(1);
      int n2 = B.GetLength(0);
      int m2 = B.GetLength(1);


      if (n1 != m1 || n1 != n2 || n2 != m2) return;

      int aInd = FindDiagonalMaxIndex(A);
      int bInd = FindDiagonalMaxIndex(B);

      SwapRowColumn(A, aInd, B, bInd);

      // end

    }

    public int CountPositiveElementsInRow(int[,] matrix, int row){

      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);
      if (n == 0 || m == 0) return 0;

      int count = 0;

      for (int i = 0; i < n; i++){
	int local = 0;
	for (int j = 0; j < m; j++){
	  if (matrix[i, j] > 0){
	    local++;
	  }
	}
	count = Math.Max(local, count);
      }
      return count;

    }


    public int CountPositiveElementsInColumn(int[,] matrix, int col){
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);
      if (n == 0 || m == 0) return 0;

      int count = 0;

      for (int i = 0; i < m; i++){
	int local = 0;
	for (int j = 0; j < n; j++){
	  if (matrix[j, i] > 0){
	    local++;
	  }
	}
	count = Math.Max(local, count);
      }
      return count;

    }

    public void InsertColumn(ref int[,] A, int rowIndex, int columnIndex, int[,] B)
    {

      int n1 = A.GetLength(0);
      int m1 = A.GetLength(1);
      int n2 = B.GetLength(0);
      int m2 = B.GetLength(1);

      int[,] Ans = new int[n1+1,m1];
      for (int i = 0; i < n1; i++)
      {
	for (int j = 0; j < m1; j++){
	  if (i > rowIndex+1){
	    Ans[i+1, j] = A[i, j];
	  }
	  else if (i == rowIndex+1) {
	    Ans[i, j] = B[j, columnIndex];
	  }
	  else{
	    Ans[i, j] = A[i, j];
	  }
	}
      }
      A = Ans;

    }

    public void Task2(ref int[,] A, int[,] B)
    {

      // code here

      int n1 = A.GetLength(0);
      int m1 = A.GetLength(1);
      int n2 = B.GetLength(0);
      int m2 = B.GetLength(1);

      if (m1 != n2) return;

      int colInd = 0;
      int colStat = 0;
      int rowInd = 0;
      int rowStat = 0;

      for (int i = 0; i < n2; i++){
	int cur_state = CountPositiveElementsInColumn(B, i);
	if (cur_state > colStat){
	  colInd = i;
	  colStat = cur_state;
	}
      }

      for (int i = 0; i < m1; i++){
	int cur_state = CountPositiveElementsInColumn(A, i);
	if (cur_state > rowStat){
	  rowInd = i;
	  rowStat = cur_state;
	}
      }

      if (colStat == 0) return;

      InsertColumn(ref A, rowInd, colInd, B);

      // end

    }

    public void ChangeMatrixValues(int[,] matrix)
    {
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      int[] maxesx = new int[5];
      int[] maxesy = new int[5];

      if (n*m >= 5)
      {

	for (int i = 0; i < n; i++){
	  for (int j = 0; j < m; j++){
	    if (matrix[maxesx[0], maxesy[0]] < matrix[i, j]){
	      maxesx[0] = i;
	      maxesy[0] = j;
	      int k = 0;
	      while (k < 5 && matrix[maxesx[k], maxesy[k]] > matrix[maxesx[k+1], maxesy[k+1]])
	      {
		(maxesx[k], maxesx[k+1], maxesy[k], maxesy[k+1]) = (maxesx[k+1], maxesx[k], maxesy[k+1], maxesy[k]);
	      }
	    }
	  }
	}

	for (int i = 0; i < 5; i++){
	  matrix[maxesx[i], maxesy[i]]*=4;
	}
	for (int i = 0; i < n; i++){
	  for (int j = 0; j < m; j++){
	    matrix[i, j]/=2;
	  }
	}
      }
      else
      {
	for (int i = 0; i < n; i++){
	  for (int j = 0; j < m; j++){
	    matrix[i, j]*=2;
	  }
	}
      }
    }

    public void Task3(int[,] matrix)
    {

      // code here

      ChangeMatrixValues(matrix);

      // end

    }

    public int[] CountNegativesPerRow(int[,] matrix)
    {
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      int[] ans = new int[n];

      for (int i = 0; i < n; i++){
	int count = 0;
	for (int j = 0; j < m; j++){
	  if (matrix[i, j] < 0){
	    count++;
	  }
	}
	ans[i] = count;
      }
      return ans;
    }

    public int FindMaxIndex(int[] array)
    {
      int imax = 0;
      for (int i = 0; i < array.Length; i++){
	if (array[imax] < array[i]){
	  imax = i;
	}
      }
      return imax;
    }

    public void Task4(int[,] A, int[,] B)
    {

      // code here
      int n1 = A.GetLength(0);
      int m1 = A.GetLength(1);
      int n2 = B.GetLength(0);
      int m2 = B.GetLength(1);

      int[] negA = CountNegativesPerRow(A);
      int[] negB = CountNegativesPerRow(B);

      int rowIndA = FindMaxIndex(negA);
      int rowIndB = FindMaxIndex(negB);

      for (int i = 0; i < Math.Min(m1, m2); i++){
	(A[rowIndA, i], B[rowIndB, i]) = (B[rowIndB, i], A[rowIndA, i]);
      }
      // end

    }

    public void SortNegativeDescending(int[] matrix)
    {
      int count = 0;
      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  count++;
	}
      }

      int[] negatives = new int[count];

      int done = 0;
      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  negatives[done++] = matrix[i];
	}
      }

      Array.Sort(negatives);

      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  matrix[i] = negatives[done--];
	}
      }
    }

    public void SortNegativeAscending(int[] matrix)
    {
      int count = 0;
      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  count++;
	}
      }

      int[] negatives = new int[count];

      int done = 0;
      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  negatives[done++] = matrix[i];
	}
      }

      Array.Sort(negatives);
      done = 0;
      for (int i = 0; i < matrix.Length; i++){
	if (matrix[i] < 0){
	  matrix[i] = negatives[done++];
	}
      }
    }

    public delegate void Sorting(int[] matrix);
    public void Task5(int[] matrix, Sorting sort)
    {

      // code here

      sort(matrix);

      // end

    }

    public delegate void SortRowsByMax(int[,] matrix);

    public void SwapRows(int ind1, int ind2, ref int[,] matrix)
    {
      
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      for (int j = 0; j < m; j++){
	(matrix[ind1, j], matrix[ind2, j]) = (matrix[ind2, j], matrix[ind1, j]);
      }
    }

    public void SortRowsByMaxAscending(int[,] matrix)
    {
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      int[] maxes = new int[n];
      for (int i = 0; i < n; i++){
	int lmax = matrix[i, 0];
	for (int j = 0; j < m ;j++){
	  if (matrix[i, j] > lmax){
	    lmax = matrix[i, j];
	  }
	}
	maxes[i] = lmax;
      }

      int k = 1;
      while (k < n){
	if (k == 0 || maxes[k] >= maxes[k - 1]) 
	  k++;
	else{
	  (maxes[k], maxes[k - 1]) = (maxes[k - 1], maxes[k]);
	  SwapRows(k, k-1, ref matrix);
	  k--;
	}
      }
    }

    public void SortRowsByMaxDescending(int[,] matrix)
    {
      int n = matrix.GetLength(0);
      int m = matrix.GetLength(1);

      int[] maxes = new int[n];
      for (int i = 0; i < n; i++){
	int lmax = matrix[i, 0];
	for (int j = 0; j < m ;j++){
	  if (matrix[i, j] > lmax){
	    lmax = matrix[i, j];
	  }
	}
	maxes[i] = lmax;
      }

      int k = 1;
      while (k < n){
	if (k == 0 || maxes[k] < maxes[k - 1]) 
	  k++;
	else{
	  (maxes[k], maxes[k - 1]) = (maxes[k - 1], maxes[k]);
	  SwapRows(k, k-1, ref matrix);
	  k--;
	}
      }
    }

    public void Task6(int[,] matrix, SortRowsByMax sort)
    {

      // code here

      // end

    }
    public int[] Task7(int[,] matrix/*, /*FindNegatives find*/)
    {
      int[] negatives = null;

      // code here

      // end

      return negatives;
    }
    public int[,] Task8(int[,] matrix/*, MathInfo info*/)
    {
      int[,] answer = null;

      // code here

      // end

      return answer;
    }
    public int Task9(double a, double b, double h, Func<double, double> func)
    {
      int answer = 0;

      // code here

      // end

      return answer;
    }
    public void Task10(int[][] array, Action<int[][]> func)
    {

      // code here

      // end

    }



  }
}
