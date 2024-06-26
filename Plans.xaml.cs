

namespace MauiApp3;

public partial class Plans : ContentPage
{
    Picker picker, fdsfds;
    CheckBox checkbox;
    public Plans()
    {
        InitializeComponent();
        picker = new Picker();
        picker.SelectedIndexChanged += UpdateData;
        using (DatabaseContext context = new DatabaseContext())
        {
            var cats = context.Categories.ToList();
            foreach (var c in cats)
                picker.Items.Add(c.Name);
        }
        Label label = new Label() { Text = "Category" };
        HorizontalStackLayout stackTopIn = new HorizontalStackLayout();
        label.Margin = new Thickness(0, 10, 0, 0);
        stackTopIn.Add(label);
        stackTopIn.Add(picker);
        stackTopIn.HorizontalOptions = LayoutOptions.Center;
        stackTop.Add(stackTopIn);
    }
    private void UpdateData(object sender, System.EventArgs e)
    {
        stackBottom.Clear();
        using (DatabaseContext context = new DatabaseContext())
        {
            var cat = context.Categories.FirstOrDefault(u => u.Name == picker.SelectedItem.ToString());
            var items = context.Plans.Where(p => p.Category == cat).ToList();
            foreach (var plan in items)
                showItem(plan, false);
        }
    }

    private void ShowGoals(object sender, System.EventArgs e)
    {
        stackBottom.Clear();
        using (DatabaseContext context = new DatabaseContext())
        {
            var plan = context.Plans.FirstOrDefault(u => u.Id == Convert.ToInt32(((Button)sender).StyleId));
            var goals = context.Goals.Where(p => p.plan == plan).ToList();
            int i = 0;
            showItem(plan, true);
            foreach (var item2 in goals)
            {
                VerticalStackLayout stackGoal = new VerticalStackLayout();
                i++;
                stackGoal.Spacing = 5;
                Label label5 = new();
                Label label6 = new();
                label5.Text = i.ToString() + ".  " + item2.Name;
                label6.Text = item2.Description;
                checkbox = new();
                checkbox.StyleId = item2.Id.ToString();
                if (item2.Status) checkbox.IsChecked = true;
                checkbox.CheckedChanged += checkUpdate;
                Label label7 = new();
                label7.Text = "Done";
                HorizontalStackLayout stackIn = new();
                label7.Margin = new Thickness(0, 10, 0, 0);
                stackIn.Add(checkbox); stackIn.Add(label7);
                stackGoal.Add(label5); stackGoal.Add(label6);
                stackGoal.Add(stackIn);
                stackBottom.Add(stackGoal);
            }
        }
    }

    void showItem(Plan item0, bool flag)
    {
        using (DatabaseContext context = new DatabaseContext())
        {
            var plan = context.Plans.Find(item0.Id);
            VerticalStackLayout stackItem = new VerticalStackLayout();
            stackItem.Spacing = 10;
            Label label1 = new();
            label1.Text = plan.Name;
            label1.FontSize = 20;
            Label label2 = new();
            label2.Text = "Term : " + plan.Type;
            label2.Text += ";  Date : " + plan.Data;
            Label label3 = new();
            label3.Text = "Progress : ";
            var goals = context.Goals.Where(p => p.plan == plan).ToList();
            var goals2 = context.Goals.
                Where(p => p.plan == plan).
                Where(p => p.Status == true).ToList();
            ProgressBar progress = new ProgressBar()
            {
            };

            if (goals.Count > 0)
            {
                progress.Progress = (double)goals2.Count / (double)goals.Count;
            }
            progress.WidthRequest = 400;
            progress.HeightRequest = 4;
            Button button = new();
            if (flag)
            {
                button.Text = "to Category";
                button.Clicked += UpdateData;
            }
            else
            {
                button.Text = "Show Goals";
                button.StyleId = plan.Id.ToString();
                button.Clicked += ShowGoals;
            }
            stackItem.Add(label1);
            stackItem.Add(label2);
            stackItem.Add(label3);
            stackItem.Add(progress);
            stackItem.Add(button);
            stackBottom.Add(stackItem);
        }
    }
    private void checkUpdate(object sender, System.EventArgs e)
    {
        using (DatabaseContext context = new DatabaseContext())
        {
            var goal = context.Goals.Find(Convert.ToInt32(((CheckBox)sender).StyleId));
            if (goal.Status == true)
                goal.Status = false;
            else
                goal.Status = true;
            context.SaveChanges();
        }
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage() { });
    }
    private void Accounting(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Accounting());
    }
    private void Settings(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Settings());
    }
}