using System;
using System.Collections.Generic;
using Company.Project.Features.Items;
using Company.Project.UI;

namespace Company.Project.Features.Inventory
{
    public interface IInventoryController
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void ShowInventory(Action hideAction);
        void HideInventory();
    }
    
    public interface IInventoryModel
    {
        IReadOnlyList<IItem> GetEquippedItems();
        void EquipItem(IItem item);
        void UnequipItem(IItem item);
    }
    
    public interface IInventoryView : IView
    {
        event EventHandler<IItem> Selected;
        event EventHandler<IItem> Deselected;
        void Display(List<IItem> itemInfoCollection);
    }
}