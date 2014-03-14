using Spellistaren.model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spellistaren.model
{
    public class Service
    {
        private ListDAL _ListDAL;
        private GameDAL _GameDAL;

        private ListDAL ListDAL
        {
            get { return _ListDAL ??  (_ListDAL = new ListDAL()); } //om _ListDAL är Null, sätt ListDAL till ny ListDAL...
        }
        private GameDAL GameDAL
        {
            get { return _GameDAL ?? (_GameDAL = new GameDAL()); } //om _GameDAL är Null, sätt ListDAL till ny GameDAL...
        }

        public IEnumerable<Game> GetListContent(int ListID) 
        {
            var Listcontent =  ListDAL.GetListContent(ListID, 1); //hårdkodar in värdet 1 då programmet är begränsat till 1 användare..
            var ListcontentAndName = new List<Game>();
            Game GameDet;
            for (var i = 0; i < Listcontent.Count; i++ )
            {
                GameDet = GameDAL.GetGameDetails(1, Listcontent[i].GameID); //hämtar ut spelet som finns på listan.
                ListcontentAndName.Add(GameDet); //lägger in spelet i listan..
            }

            return ListcontentAndName; //returnerar alla spelen i listan...
        }

        public IEnumerable<List> GetLists()
        {
            return ListDAL.GetLists(1); //Hårdkodar in värdet 1 här, då programmet blir begränsatt till 1 användare...
        }
    }
}