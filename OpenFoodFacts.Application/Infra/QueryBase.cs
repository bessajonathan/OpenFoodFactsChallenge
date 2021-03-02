namespace OpenFoodFacts.Application.Infra
{
    public class QueryBase
    {

        /// <summary>
        /// Número da página a ser consultada. Se não informar nada, será usada a página 1.
        /// </summary>
        /// <example>1</example>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Número de registros por página. Se não informar nada, será usada a quantidade padrão de 10 registros por página.
        /// </summary>
        /// <example>10</example>
        public int PageSize { get; set; } = 10;

        private string _orderBy;
        /// <summary>
        /// Indicador de ordenação da consulta
        /// Exemplos: createdAt, createdAt desc, customer.document, customer.document asc
        /// </summary>
        /// <example>createAt desc</example>
        public string OrderBy
        {
            get
            {
                if (_orderBy == null || _orderBy == string.Empty)
                    return "imported_t";

                return _orderBy;
            }
            set => _orderBy = value;
        }
    }
}
