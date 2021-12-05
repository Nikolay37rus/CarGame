using System.Collections.Generic;
using Company.Project.Content;

namespace Company.Project.Features.Shed
{
    public class UpgradeHandlersRepository : BaseController, IRepository<int, IUpgradeHandler>
    {
        private readonly Dictionary<int, IUpgradeHandler> _upgradeItemsMapById = new Dictionary<int, IUpgradeHandler>();

        public IReadOnlyDictionary<int, IUpgradeHandler> Collection => _upgradeItemsMapById;
        
        public UpgradeHandlersRepository(
            List<UpgradeItemConfig> upgradeItemConfigs)
        {
            PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);
        }

        private void PopulateItems(
            ref Dictionary<int, IUpgradeHandler> upgradeHandlersMapByType,
            List<UpgradeItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (upgradeHandlersMapByType.ContainsKey(config.Id)) 
                    continue;
                
                upgradeHandlersMapByType.Add(config.Id, CreateHandlerByType(config));
            }
        }

        private IUpgradeHandler CreateHandlerByType(UpgradeItemConfig config)
        {
            switch (config.type)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeHandler(config.value);
                default:
                    return StubUpgradeHandler.Default;
            }
        }

        protected override void OnDispose()
        {
            _upgradeItemsMapById.Clear();
        }
    }
}