using System.Collections.ObjectModel;
using Todo2788307.Data;
using Todo2788307.Models;

namespace Todo2788307.Views;

public partial class TodoListPage : ContentPage
{

    TodoItemDatabase database;
    public ObservableCollection<TodoItem> Items { get; set; } = new();

	public TodoListPage(TodoItemDatabase todoItemDatabase)
	{
		InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }

    async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new TodoItem()
        });
    }
}