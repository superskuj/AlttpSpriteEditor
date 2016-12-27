using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlttpSpriteEditor
{
    public class SearchBoxTools
    {
        public static List<String> ListBoxRefinement(List<String> originalList, String searchString)
        {
            List<String> refinedList = new List<String>();

            for (int i = 0; i < originalList.Count; i++)
            {
                if (originalList[i].ToLower().Contains(searchString))
                {
                    refinedList.Add(originalList[i]);
                }
            }
            return refinedList;
        }
    }
}