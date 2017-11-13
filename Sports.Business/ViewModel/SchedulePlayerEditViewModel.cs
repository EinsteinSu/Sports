using System.Collections.Generic;

namespace Sports.Business.ViewModel
{
    public class SchedulePlayerEditViewModel
    {
        public int ScheduleId { get; set; }

        public string Title { get; set; }

        public int TeamAId { get; set; }

        public string TeamAName { get; set; }

        public int TeamBId { get; set; }

        public string TeamBName { get; set; }

        public IEnumerable<ScheduleTeamPlayerViewModel> TeamAPlayers { get; set; }

        public IEnumerable<ScheduleTeamPlayerViewModel> TeamBPlayers { get; set; }
    }

    public class ScheduleTeamPlayerViewModel
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}