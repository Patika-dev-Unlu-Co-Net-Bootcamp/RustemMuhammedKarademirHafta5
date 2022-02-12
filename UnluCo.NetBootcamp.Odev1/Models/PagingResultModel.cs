using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Models
{
    public class PagingResultModel<T> : List<T>
    {
        public QueryParams Params { get; }
        public PagingResponseModel Result { get; set; }

        public PagingResultModel(QueryParams queryParams)
        {
            Params = queryParams;
            Result = new PagingResponseModel();
        }

        public void GetData(IQueryable<T> query)
        {
            var result = query.ToList();
            
            if (!string.IsNullOrWhiteSpace(Params.SearchingWord) && !string.IsNullOrWhiteSpace(Params.SearchingFilter))//Arama filtresi ve arama kelimesi varsa filtreleme yapar
            {
                var entity = typeof(T);//query hangi tur oldugunu verir

                var property = entity.GetProperty(Params.SearchingFilter);//query propertylerinden queryParams ile geleni bulur

                result = result.Where<T>(x => property.GetValue(x, null).ToString().ToUpper().Contains(Params.SearchingWord.ToUpper())).ToList();//filtre alanina gore arama yapar
            }
            if ((!string.IsNullOrWhiteSpace(Params.MinValue.ToString()) || !string.IsNullOrWhiteSpace(Params.MaxValue.ToString())) && (Params.SearchingFilter == "ModelYear"))//Arama filtresi ModelYear ve min veya max deger girilmis ise filtreleme yapar
            {
                var entity = typeof(T);

                var property = entity.GetProperty(Params.SearchingFilter);

                result = result.Where<T>(x => (int)property.GetValue(x, null) >= Params.MinValue && (int)property.GetValue(x, null) <= Params.MaxValue).ToList();
            }
            if (!string.IsNullOrWhiteSpace(Params.Sort))//Sort degerine gore siralama yapar
            {
                var entity = typeof(T);

                var property = entity.GetProperty(Params.Sort);

                if ((int)Params.SortingDirection == 2)
                    result = result.OrderByDescending(x => property.GetValue(x, null)).ToList();
                else
                    result = result.OrderBy(x => property.GetValue(x, null)).ToList();
            }

            Result.TotalCount = result.Count();
            Result.PageSize = Params.PageSize;
            Result.TotalPages = (int)Math.Ceiling(Result.TotalCount / (double)Params.PageSize);
            Result.CurrentPage = Params.Page;
            Result.NextPage = Result.TotalPages > Result.CurrentPage ? Result.CurrentPage + 1 : Result.CurrentPage;
            Result.PreviousPage = Result.CurrentPage == 1 ? Result.CurrentPage : Result.CurrentPage - 1;

            result = result.Skip((Params.Page - 1) * Params.PageSize)
                  .Take(Params.PageSize).ToList();
            AddRange(result);
        }
    }
}
