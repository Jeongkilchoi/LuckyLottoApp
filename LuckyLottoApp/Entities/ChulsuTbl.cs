

using System.ComponentModel.DataAnnotations;

namespace LuckyLottoApp.Entities
{
    public class ChulsuTbl
    {
        [Key]
        public int Orders { get; set; }
        /// <summary>
        /// 정렬당번[01]합
        /// </summary>
        public int Danhap1 { get; set; }
        /// <summary>
        /// 정렬당번[23]합
        /// </summary>
        public int Danhap2 { get; set; }
        /// <summary>
        /// 정렬당번[45]합
        /// </summary>
        public int Danhap3 { get; set; }
        /// <summary>
        /// 정렬당번[05]합
        /// </summary>
        public int Danhap4 { get; set; }
        /// <summary>
        /// 정렬당번[12]합
        /// </summary>
        public int Danhap5 { get; set; }
        /// <summary>
        /// 정렬당번[34]합
        /// </summary>
        public int Danhap6 { get; set; }
        /// <summary>
        /// 정렬당번[012]합
        /// </summary>
        public int Danhap7 { get; set; }
        /// <summary>
        /// 정렬당번[345]합
        /// </summary>
        public int Danhap8 { get; set; }
        /// <summary>
        /// 정렬당번[024]합
        /// </summary>
        public int Danhap9 { get; set; }
        /// <summary>
        /// 정렬당번[135]합
        /// </summary>
        public int Danhap10 { get; set; }
        public int Danhap11 { get; set; }
        public int Danhap12 { get; set; }
        public int Sunhap1 { get; set; }
        public int Sunhap2 { get; set; }
        public int Sunhap3 { get; set; }
        public int Sunhap4 { get; set; }
        public int Sunhap5 { get; set; }
        public int Sunhap6 { get; set; }
        public int Sunhap7 { get; set; }
        public int Sunhap8 { get; set; }
        public int Sunhap9 { get; set; }
        public int Sunhap10 { get; set; }
        public int Sunhap11 { get; set; }
        public int Sunhap12 { get; set; }
        /// <summary>
        /// 당번의 합
        /// </summary>
        public int Hapgey { get; set; }
        /// <summary>
        /// 당번의 앞자리 합
        /// </summary>
        public int Aphap { get; set; }
        /// <summary>
        /// 당번의 뒤자리 합
        /// </summary>
        public int Dwihap { get; set; }
        /// <summary>
        /// 앞자리 합 + 뒷자리 합
        /// </summary>
        public int Apdwihap { get; set; }
        /// <summary>
        /// 당번의 최대값 - 최소값
        /// </summary>
        public int Gojeocha { get; set; }
        /// <summary>
        /// 당번의 최소값 + 최대값
        /// </summary>
        public int Gojeohap { get; set; }
        /// <summary>
        /// 정렬당번[01] 차이값
        /// </summary>
        public int Dgapcha1 { get; set; }
        /// <summary>
        /// 정렬당번[12] 차이값
        /// </summary>
        public int Dgapcha2 { get; set; }
        /// <summary>
        /// 정렬당번[23] 차이값
        /// </summary>
        public int Dgapcha3 { get; set; }
        /// <summary>
        /// 정렬당번[34] 차이값
        /// </summary>
        public int Dgapcha4 { get; set; }
        /// <summary>
        /// 정렬당번[45] 차이값
        /// </summary>
        public int Dgapcha5 { get; set; }
        public int Sgapcha1 { get; set; }
        public int Sgapcha2 { get; set; }
        public int Sgapcha3 { get; set; }
        public int Sgapcha4 { get; set; }
        public int Sgapcha5 { get; set; }
        public int Dgaphap1 { get; set; }
        public int Dgaphap2 { get; set; }
        public int Dgaphap3 { get; set; }
        public int Dgaphap4 { get; set; }
        public int Dgaphap5 { get; set; }
        public int Sgaphap1 { get; set; }
        public int Sgaphap2 { get; set; }
        public int Sgaphap3 { get; set; }
        public int Sgaphap4 { get; set; }
        public int Sgaphap5 { get; set; }
        /// <summary>
        /// 정렬당번[0] 끝
        /// </summary>
        public int Dankkeut1 { get; set; }
        /// <summary>
        /// 정렬당번[1] 끝
        /// </summary>
        public int Dankkeut2 { get; set; }
        /// <summary>
        /// 정렬당번[2] 끝
        /// </summary>
        public int Dankkeut3 { get; set; }
        /// <summary>
        /// 정렬당번[3] 끝
        /// </summary>
        public int Dankkeut4 { get; set; }
        /// <summary>
        /// 정렬당번[4] 끝
        /// </summary>
        public int Dankkeut5 { get; set; }
        /// <summary>
        /// 정렬당번[05 끝
        /// </summary>
        public int Dankkeut6 { get; set; }
        public int Sunkkeut1 { get; set; }
        public int Sunkkeut2 { get; set; }
        public int Sunkkeut3 { get; set; }
        public int Sunkkeut4 { get; set; }
        public int Sunkkeut5 { get; set; }
        public int Sunkkeut6 { get; set; }
        public int Beisu4 { get; set; }
        public int Beisu5 { get; set; }
        public int Beisu6 { get; set; }
        public int Beisu7 { get; set; }
        public int Beisu8 { get; set; }
        public int Beisu9 { get; set; }
        /// <summary>
        /// 쌍둥이 갯수
        /// </summary>
        public int Donhyeong { get; set; }
        public int Acval1 { get; set; }
        public int Acval2 { get; set; }
        public int Acval3 { get; set; }
        public int Acval4 { get; set; }
        /// <summary>
        /// 제곱수
        /// </summary>
        public int Jekobsu { get; set; }
        /// <summary>
        /// 이항계수
        /// </summary>
        public int Ihangsu { get; set; }
        /// <summary>
        /// 피보나치수
        /// </summary>
        public int Pivosu { get; set; }
        /// <summary>
        /// 역피보나치수
        /// </summary>
        public int Revpivo { get; set; }
        /// <summary>
        /// 뜨거운수
        /// </summary>
        public int? Hotsu { get; set; }
        /// <summary>
        /// 미지근수
        /// </summary>
        public int? Warmsu { get; set; }
        /// <summary>
        /// 차가운수
        /// </summary>
        public int? Coldsu { get; set; }
        /// <summary>
        /// 달력수
        /// </summary>
        public int? Daluksu { get; set; }
        /// <summary>
        /// 이웃수
        /// </summary>
        public int? Ihutsu { get; set; }
        /// <summary>
        /// 빈수
        /// </summary>
        public int? Binsu { get; set; }
        /// <summary>
        /// 호주당번 중복수
        /// </summary>
        public int? Autsu { get; set; }
        /// <summary>
        /// 캐나다당번 중복수
        /// </summary>
        public int? Cansu { get; set; }
        /// <summary>
        /// 삼각수1
        /// </summary>
        public int? Samkak1 { get; set; }
        /// <summary>
        /// 삼각수2
        /// </summary>
        public int? Samkak2 { get; set; }
        /// <summary>
        /// 삼각수3
        /// </summary>
        public int? Samkak3 { get; set; }
        /// <summary>
        /// 삼각수4
        /// </summary>
        public int? Samkak4 { get; set; }
        /// <summary>
        /// 정낙수1
        /// </summary>
        public int? Snaksu1 { get; set; }
        /// <summary>
        /// 정낙수2
        /// </summary>
        public int? Snaksu2 { get; set; }
        /// <summary>
        /// 정낙수3
        /// </summary>
        public int? Snaksu3 { get; set; }
        /// <summary>
        /// 정낙수4
        /// </summary>
        public int? Snaksu4 { get; set; }
        /// <summary>
        /// 정낙수5
        /// </summary>
        public int? Snaksu5 { get; set; }
        /// <summary>
        /// 정낙수6
        /// </summary>
        public int? Snaksu6 { get; set; }
        /// <summary>
        /// 역낙수1
        /// </summary>
        public int? Rnaksu1 { get; set; }
        /// <summary>
        /// 역낙수2
        /// </summary>
        public int? Rnaksu2 { get; set; }
        /// <summary>
        /// 역낙수3
        /// </summary>
        public int? Rnaksu3 { get; set; }
        /// <summary>
        /// 역낙수4
        /// </summary>
        public int? Rnaksu4 { get; set; }
        /// <summary>
        /// 역낙수5
        /// </summary>
        public int? Rnaksu5 { get; set; }
        /// <summary>
        /// 역낙수6
        /// </summary>
        public int? Rnaksu6 { get; set; }
        public int Dway1 { get; set; }
        public int Dway2 { get; set; }
        public int Dway3 { get; set; }
        public int Dway4 { get; set; }
        public int Dway5 { get; set; }
        public int Sway1 { get; set; }
        public int Sway2 { get; set; }
        public int Sway3 { get; set; }
        public int Sway4 { get; set; }
        public int Sway5 { get; set; }
        public int Updw1 { get; set; }
        public int Updw2 { get; set; }
        public int Updw3 { get; set; }
        public int Updw4 { get; set; }
        public int Updw5 { get; set; }
        public int Updw6 { get; set; }
        public int Updw7 { get; set; }
        public int Updw8 { get; set; }
        public int Updw9 { get; set; }
    }
}
