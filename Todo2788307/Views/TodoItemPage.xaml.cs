using Todo2788307.Data;
using Todo2788307.Models;

namespace Todo2788307.Views;


[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
    TodoItemPage item;

    public TodoItem Item
    {
        get => BindingContext as TodoItem;
        set => BindingContext = value;
    }

    TodoItemDatabase database;
	public TodoItemPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;
	}

    async void Button_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

   async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");

    }

    async void Button_Clicked_2(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}