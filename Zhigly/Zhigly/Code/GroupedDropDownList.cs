using System.Web;
using System.Web.UI.WebControls;

namespace Zhigly.Code
{
    public class GroupedDropDownList : DropDownList
    {
        public void AddItemGroup(string groupTitle)
        {
            Items.Add(new ListItem(groupTitle, "$$OPTGROUP$$OPTGROUP$$"));
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            if (Items.Count > 0)
            {
                bool selected = false;
                bool optGroupStarted = false;

                for (int i = 0; i < Items.Count; i++)
                {
                    ListItem item = Items[i];

                    if (item.Enabled)
                    {
                        if (item.Value == "$$OPTGROUP$$OPTGROUP$$")
                        {
                            if (optGroupStarted)
                            {
                                writer.WriteEndTag("optgroup");
                            }
                            writer.WriteBeginTag("optgroup");
                            writer.WriteAttribute("label", item.Text);
                            writer.Write('>');
                            writer.WriteLine();
                            optGroupStarted = true;
                        }
                        else
                        {
                            writer.WriteBeginTag("option");

                            if (item.Selected)
                            {
                                if (selected)
                                {
                                    VerifyMultiSelect();
                                }
                                selected = true;
                                writer.WriteAttribute("selected", "selected");
                            }

                            writer.WriteAttribute("value", item.Value, true);

                            if (item.Attributes.Count > 0)
                            {
                                item.Attributes.Render(writer);
                            }

                            if (Page != null)
                            {
                                Page.ClientScript.RegisterForEventValidation(UniqueID, item.Value);
                            }

                            writer.Write('>');
                            HttpUtility.HtmlEncode(item.Text, writer);
                            writer.WriteEndTag("option");
                            writer.WriteLine();
                        }
                    }
                }

                if (optGroupStarted)
                {
                    writer.WriteEndTag("optgroup");
                }
            }
        }
    }
}