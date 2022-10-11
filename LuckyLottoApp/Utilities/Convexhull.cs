namespace LuckyLottoApp.Utilities
{
    /// <summary>
    /// 볼록 외곽선 검사하는 클래스(소수점 자리수는 4개로 맞춤(정수부분3 + 소수점부분4 합이 7))
    /// </summary>
    public static class Convexhull
    {
        /// <summary>
        /// ConvexHull 계산에 사용함
        /// </summary>
        /// <param name="O"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        private static double Cross(Point O, Point A, Point B)
        {
            return (A.X - O.X) * (B.Y - O.Y) - (A.Y - O.Y) * (B.X - O.X);
        }

        /// <summary>
        /// 볼록 외곽선 계산 결과를 반환
        /// </summary>
        /// <param name="points">좌표 리스트 (point.X: 번호의 행인덱스, point.Y: 번호의 열인덱스)</param>
        /// <returns>외곽선 좌표 리스트</returns>
        public static List<Point> GetConvexHull(List<Point> points)
        {
            if (points == null)
            {
                return new List<Point>();
            }
            else if (points.Count() <= 1)
            {
                return points;
            }
            else
            {
                List<Point> copy = new List<Point>(points);
                int n = copy.Count(), k = 0;

                List<Point> hull = new List<Point>(new Point[2 * n]);

                copy.Sort((a, b) => a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

                // Build lower hull
                for (int i = 0; i < n; ++i)
                {
                    while (k >= 2 && Cross(hull[k - 2], hull[k - 1], copy[i]) <= 0)
                    {
                        k--;
                    }

                    hull[k++] = copy[i];
                }

                // Build upper hull
                for (int i = n - 2, t = k + 1; i >= 0; i--)
                {
                    while (k >= t && Cross(hull[k - 2], hull[k - 1], copy[i]) <= 0)
                    {
                        k--;
                    }

                    hull[k++] = copy[i];
                }

                return hull.Take(k - 1).ToList();
            }
        }

        /// <summary>
        /// 2 선분의 각도
        /// </summary>
        /// <param name="points">좌표 리스트</param>
        /// <returns>가도배열 (실수)</returns>
        public static float[] CalculateAngle(List<Point> points)
        {
            //외곽선 알고리즘엔 첫번째 포인트가 추가되어 있기 때문에 마지막을 삭제
            var pts = points.Take(points.Count() - 1).ToList();

            var lst = new List<float>();

            //포인트 리스트를 3개씩 짝이루기
            for (int i = 0; i < pts.Count; i++)
            {
                Point[] arr = new Point[3];

                for (int j = 0; j < 3; j++)
                {
                    int n = (i + j) < pts.Count ? (i + j) : Math.Abs(pts.Count - (i + j));
                    arr[j] = pts[n];
                }

                var p2 = arr[0];
                var p1 = arr[1];
                var p3 = arr[2];
                double numerator = p2.Y * (p1.X - p3.X) + p1.Y * (p3.X - p2.X) + p3.Y * (p2.X - p1.X);
                double denominator = (p2.X - p1.X) * (p1.X - p3.X) + (p2.Y - p1.Y) * (p1.Y - p3.Y);

                double ratio = numerator / denominator;

                double angleRad = Math.Atan(ratio);
                double angleDeg = (angleRad * 180) / Math.PI;
                float angle = angleDeg < 0 ? 180 + (float)angleDeg : (float)angleDeg;

                if (double.IsNaN(angle))
                    lst.Add(0);
                else
                    lst.Add(angle);
            }

            var rts = new float[6];

            for (int i = 0; i < 6; i++)
            {
                if (i < lst.Count)
                    rts[i] = (float)Math.Round(lst[i], 4);
                else
                    rts[i] = 0;
            }

            return rts;
        }

        /// <summary>
        /// 당번 사이의 각도 리스트
        /// </summary>
        /// <param name="points">좌표 리스트</param>
        /// <returns>각도배열 (실수)</returns>
        public static float[] AngleBetweens(List<Point> points)
        {
            var rst = new List<float>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                Point pt1 = points[i];
                Point pt2 = points[i + 1];
                var angle = AngleBetweenPoint(pt1, pt2);
                rst.Add(angle);
            }

            return rst.ToArray();
        }

        /// <summary>
        /// 2 포인트의 각도
        /// </summary>
        /// <param name="start">시작 좌표</param>
        /// <param name="end">종료 좌표</param>
        /// <returns>각도 (실수)</returns>
        private static float AngleBetweenPoint(Point start, Point end)
        {
            double dy = end.Y - start.Y;
            double dx = end.X - start.X;
            double angle = Math.Atan2(dy, dx) * (180.0 / Math.PI);

            return (float)Math.Round(angle, 4);
        }

        /// <summary>
        /// 배열의 인덱스를 포인트로 반환
        /// </summary>
        /// <param name="array">정수배열</param>
        /// <returns>포인트</returns>
        private static Point ArrayToPoint(int[] array)
        {
            if (array.Length != 2)
            {
                throw new Exception("행과 열 인덱스 2요소 배열");
            }

            //배열은 행의 인덱스와 열의 인덱스로 이루어진 것을 포인트로 바꾸기 위해
            //포인트 X 는 열인덱스,  Y 는 행의 인덱스로 바꿈
            return new Point(array[1], array[0]);
        }

        /// <summary>
        /// 두 점간의 거리 집합
        /// </summary>
        /// <param name="point1">좌표1</param>
        /// <param name="point2">좌표2</param>
        /// <returns>실수 배열</returns>
        public static float[] DistancePoints(List<Point> pts)
        {
            var lines = new List<float>();

            for (int i = 1; i <= 6; i++)
            {
                if (i < pts.Count())
                {
                    float lne = DistancePoint(pts[i - 1], pts[i]);
                    lines.Add(lne);
                }
                else
                {
                    lines.Add(0);
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// 두 점간의 거리
        /// </summary>
        /// <param name="point1">좌표1</param>
        /// <param name="point2">좌표2</param>
        /// <returns>거리 (실수)</returns>
        private static float DistancePoint(Point point1, Point point2)
        {
            var dt = Math.Sqrt(Math.Pow((point2.Y - point1.Y), 2) + Math.Pow((point2.X - point1.X), 2));
            return (float)Math.Round(dt, 4);
        }

        /// <summary>
        /// 좌표집합의 면적을 반환
        /// </summary>
        /// <param name="pnts">포인트 리스트</param>
        /// <returns>면적 (실수)</returns>
        public static float Area(List<Point> pnts)
        {
            var result = pnts.Take(pnts.Count - 1)
                             .Select((p, i) => (pnts[i + 1].X - p.X) * (pnts[i + 1].Y + p.Y))
                             .Sum() / 2;

            var d = Math.Abs(result);
            return  d;
        }

        /// <summary>
        /// 번호의 가로,세로 인덱스를 포인트로 반환
        /// </summary>
        /// <param name="number">번호</param>
        /// <param name="datas">배열 리스트</param>
        /// <returns>포인트</returns>
        public static Point IndexToPoint(int number, List<int[]> datas)
        {
            //먼저 행에 해당하는 인덱스를 찾기
            int yIndex = datas.FindIndex(v => v.Contains(number));
            int xIndex = Array.IndexOf(datas[yIndex], number);
            var point = new Point(yIndex, xIndex);

            return point;
        }

        /// <summary>
        /// 당번의 포인트 리스트 (당번각도 계산에만 사용)
        /// </summary>
        /// <param name="dang">정렬된 당번</param>
        /// <param name="datas">분할된 전체 데이터</param>
        /// <returns>포인트 리스트</returns>
        public static List<Point> DangbeonPosition(IEnumerable<int> dang, List<int[]> datas)
        {
            var list = new List<Point>();

            foreach (var num in dang)
            {
                int rowIndex = datas.Select((val, idx) => new { val, idx })
                                    .Where(x => x.val.Contains(num)).Select(x => x.idx).Single();

                int colIndex = datas[rowIndex].Select((val, idx) => new { val, idx })
                                              .Where(x => x.val == num).Select(x => x.idx).Single();

                int[] array = { rowIndex, colIndex };

                var point = ArrayToPoint(array);
                list.Add(point);
            }

            return list;
        }
    }
}
