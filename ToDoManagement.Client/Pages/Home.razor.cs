//using ToDoManagement.Client.Components;

//namespace ToDoManagement.Client.Pages;

//public partial class Home
//{
//    private List<ToDoItem> _toDos = [];

//    protected override void OnInitialized()
//    {
//        _toDos.Add(new ToDoItem()
//        {
//            Caption = "First ToDo"
//        });

//        base.OnInitialized();
//    }

//    protected override Task OnAfterRenderAsync(bool firstRender)
//    {
//        if (firstRender)
//        {
//            _toDos.Add(new ToDoItem()
//            {
//                Caption = "Second ToDo"
//            });
//            StateHasChanged();
//        }
//        return base.OnAfterRenderAsync(firstRender);
//    }

//    private void AddNewToDo()
//    {
//        _toDos.Add(new ToDoItem()
//        {
//            Caption = Guid.NewGuid().ToString()
//        });
//    }
//}