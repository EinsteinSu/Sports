using System.Collections.Generic;
using System.Linq;
using Sports.DataAccess.Models;

namespace Sports.Business
{
    public interface IPlayerMgr : ICrudMgr<Player>
    {
    }

    public class PlayerMgr : CrudMgrBase<Player>, IPlayerMgr
    {
        protected override string EntityName => "Player";

        protected override IEnumerable<Player> GetEntries()
        {
            return Context.Players.ToList();
        }

        protected override Player GetEntry(int id)
        {
            return Context.Players.FirstOrDefault(f => f.Id == id);
        }

        protected override void AddItem(Player item)
        {
            Context.Players.Add(item);
        }


        protected override void DeleteItem(Player item)
        {
            Context.Players.Remove(item);
        }
    }
}