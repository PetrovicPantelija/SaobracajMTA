using System.Collections.Generic;

namespace Saobracaj.Dokumenta.TrainListItem
{
    internal class Total
    {
        public decimal TotalUnitTare { get; set; } = 0;
        public decimal TotalGoods { get; set; } = 0;
        public decimal TotalWagonTare { get; set; } = 0;
        public decimal TotalWeight { get; set; } = 0;
        public decimal TotalTrainLength { get; set; } = 0;

        public Total(List<TrainListItemModel> itemList, int id_sup)
        {
            if (itemList.Count > 0)
            {
                TotalUnitTare += itemList[0].KontTara;
                TotalGoods += itemList[0].Neto;
                TotalWagonTare += itemList[0].TaraKola;
                TotalTrainLength += itemList[0].DuzinaKola;
                for (int i = 1; i < itemList.Count; i++)
                {
                    TotalUnitTare += itemList[i].KontTara;
                    TotalGoods += itemList[i].Neto;
                    if (itemList[i].RedniBroj != itemList[i - 1].RedniBroj)
                    {
                        TotalWagonTare += itemList[i].TaraKola;
                        TotalTrainLength += itemList[i].DuzinaKola;
                    }
                }
                TotalUnitTare = TotalUnitTare / 2;
                TotalWeight = TotalGoods + TotalUnitTare + TotalWagonTare;
            }
        }
    }
}
