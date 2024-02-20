using Colosoft.Presentation.Input;
using Colosoft.Presentation.Menu;

namespace Colosoft.Presentation.Test.Menu
{
    public class MenuItemsManagerTest
    {
        [Fact]
        public void GivenMenuCollectionThenBuildItems()
        {
            var menu = new MenuCollection();

            menu.CreateInserter("Menu1", "Menu1".GetFormatter(), new AbsolutePosition(1))
                .Begin("SubMenu1")
                    .Add("test2", "Test2".GetFormatter(), new DelegateCommand<string>((value) => { }), "test")
                .Close();

            menu.CreateInserter("Menu2", "Menu2".GetFormatter(), new AbsolutePosition(2));

            var menuManager = new MenuItemsManager();
            menuManager.Add(menu);

            Assert.True(menuManager.Items.Count == 2);
        }
    }
}
