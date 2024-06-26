

namespace MauiApp3;

public partial class Settings : ContentPage
{
    Entry entry, entry1,  entry3;
    Button but, but2, but3;
    DatePicker datepick;
    Picker pick, pickType;
    Plan plan;
    Editor edit;
    Label lab;
    Button button;
    Entry entryP1;
    Entry entryP2;
    Label labelP1;
    Picker pickerP, catPicker;
    Label labelP2;
    DatePicker dateP;
    Entry entryC1;
    Entry entryC2;
    Label labelC1;
    Picker pickerC;
    Label labelC2;
    DatePicker dateC;
    public Settings()
    {
        InitializeComponent();
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage() { });
    }

    private void Plans(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Plans());
    }
    private void Accounting(object sender, System.EventArgs e)
    {

        App.Current.MainPage = new NavigationPage(new Accounting());
    }
    private void PickerSelectedIndexChanged(object sender, System.EventArgs e)
    {
        stack.Clear();
        stack2.Clear();
        if (ItemPicker.SelectedItem.ToString() == "Category")
        {
            entry = new Entry() { Placeholder = "Category" };
            but = new Button() { Text = "Add Category" };
            but.Clicked += AddCategory;
            stack.Add(entry);
            stack.Add(but);
        }
        else if (ItemPicker.SelectedItem.ToString() == "Plan")
        {

            entry1 = new Entry() { Placeholder = "Name" };
            entry3 = new Entry() { Placeholder = "Goal" };
            pickType = new Picker() { };
            pickType.Items.Add("Long");
            pickType.Items.Add("Short");
            //entr2 = new Entry() { Placeholder = "òèï" };
            datepick = new DatePicker();
            pick = new Picker();
            edit = new Editor() { Placeholder = "Enter Description" };
            using (DatabaseContext context = new DatabaseContext())
            {
                var cats = context.Categories.ToList();
                foreach (var c in cats)
                    pick.Items.Add(c.Name);
            }
            but2 = new Button() { Text = "Add Plan" };
            but2.Clicked += AddItem;
            but3 = new Button() { Text = "Add Goal" };
            but3.Clicked += AddGoal;
            lab = new();
            stack.Add(entry1);
            stack.Add(pickType);
            stack.Add(pick);
            stack.Add(datepick);
            stack.Add(but2);

            stack2.Add(lab);
            stack2.Add(entry3);
            stack2.Add(edit);
            stack2.Add(but3);
        }

        else if (ItemPicker.SelectedItem.ToString() == "Cost")
        {
            entryC1 = new Entry() { Placeholder = "Name" };
            entryC2 = new Entry() { Placeholder = "Sum" };
            labelC1 = new Label() { Text = "Category" };
            pickerC = new Picker();
            labelC2 = new Label() { Text = "Date" };
            dateC = new DatePicker();
            using (DatabaseContext cont = new DatabaseContext())
            {
                var cats = cont.Categories.ToList();
                foreach (var c in cats)
                    pickerC.Items.Add(c.Name);
            }
            Button buttonC = new Button() { Text = "Add cost" };
            buttonC.Clicked += AddCost;
            stack.Add(entryC1);
            stack.Add(entryC2);
            stack.Add(labelC1);
            stack.Add(pickerC);
            stack.Add(labelC2);
            stack.Add(dateC);
            stack.Add(buttonC);
        }
        else if (ItemPicker.SelectedItem.ToString() == "Profit")
        {
            entryP1 = new Entry() { Placeholder = "Name" };
            entryP2 = new Entry() { Placeholder = "Sum" };
            labelP1 = new Label() { Text = "Category" };
            pickerP = new Picker();
            labelP2 = new Label() { Text = "Date" };
            dateP = new DatePicker();
            using (DatabaseContext cont = new DatabaseContext())
            {
                var cats = cont.Categories.ToList();
                foreach (var c in cats)
                    pickerP.Items.Add(c.Name);
            }
            Button buttonP = new Button() { Text = "Add Profit" };
            buttonP.Clicked += AddProfit;
            stack.Add(entryP1);
            stack.Add(entryP2);
            stack.Add(labelP1);
            stack.Add(pickerP);
            stack.Add(labelP2);
            stack.Add(dateP);
            stack.Add(buttonP);
        }
    }
    private void AddCategory(object sender, System.EventArgs e)
    {
        if (entry.Text != null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Category cat = new Category()
                {
                    Name = entry.Text
                };
                context.Categories.Add(cat);
                context.SaveChanges();
                DisplayAlert("Message", "Success", "Ok");
                entry.Text = "";
            }
        }
        else
            DisplayAlert("Message", "Add Category", "Îê");
    }
    private void AddItem(object sender, System.EventArgs e)
    {
        plan = new Plan();

        if (entry1.Text != null && pick.SelectedItem != null && pickType.SelectedItem != null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {

                plan.Name = entry1.Text;
                plan.Type = pickType.SelectedItem.ToString();
                plan.Category = context.Categories.FirstOrDefault(
                        p => p.Name == pick.SelectedItem.ToString());
                plan.Data = DateOnly.FromDateTime(datepick.Date);
                context.Plans.Add(plan);
                context.SaveChanges();
                DisplayAlert("Message", "Success", "Ok");
                entry1.Text = "";
                lab.Text = plan.Name;
            }
        }
        else DisplayAlert("Message", "Enter Data", "Îê");
    }

    private void AddGoal(object sender, System.EventArgs e)
    {
        if (entry3.Text != null && plan != null)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Plan it = context.Plans.Find(plan.Id);
                Goal goal = new()
                {
                    Name = entry3.Text,
                    Description = edit.Text,
                    plan = it,
                    Status = false
                };

                context.Goals.Add(goal);
                context.SaveChanges();
                DisplayAlert("Message", "Success", "Ok");
                entry3.Text = "";
                edit.Text = "";
            }
        }
        else DisplayAlert("Message", "Enter Goal and Plan first", "Îê");
    }

    private void AddCost(object sender, System.EventArgs e)
    {
        Cost cost = new Cost();

        if (entryC1.Text != null && entryC2.Text != null &&
            pickerC.SelectedItem != null)
        {
            using (DatabaseContext cont = new DatabaseContext())
            {

                cost.Name = entryC1.Text;
                Int32.TryParse(entryC2.Text, out int result);
                cost.Sum = result;
                cost.Category = cont.Categories.FirstOrDefault(
                        p => p.Name == pickerC.SelectedItem.ToString());
                cost.Data = DateOnly.FromDateTime(dateC.Date);
                cont.Costs.Add(cost);
                cont.SaveChanges();
                DisplayAlert("Message", "Success", "Ok");
                entryC1.Text = "";
                entryC2.Text = "";
            }
        }
        else DisplayAlert("Message", "Data not output", "Îê");

    }

    private void AddProfit(object sender, System.EventArgs e)
    {
        Profit profit = new Profit();

        if (entryP1.Text != null && entryP2.Text != null &&
            pickerP.SelectedItem != null)
        {
            using (DatabaseContext cont = new DatabaseContext())
            {

                profit.Name = entryP1.Text;
                Int32.TryParse(entryP2.Text, out int result);
                profit.Sum = result;
                profit.Category = cont.Categories.FirstOrDefault(
                        p => p.Name == pickerP.SelectedItem.ToString());
                profit.Data = DateOnly.FromDateTime(dateP.Date);
                cont.Profits.Add(profit);
                cont.SaveChanges();
                DisplayAlert("Message", "Success", "Ok");
                entryP1.Text = "";
                entryP2.Text = "";
            }
        }
        else DisplayAlert("Message", "Data not output", "Îê");

    }

}