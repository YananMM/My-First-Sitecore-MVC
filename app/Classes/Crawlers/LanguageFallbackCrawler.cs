using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Abstractions;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace Landmark.Classes.Crawlers
{
    public class LanguageFallbackCrawler : SitecoreItemCrawler
    {
        protected override void DoAdd(IProviderUpdateContext context, SitecoreIndexableItem indexable)
        {
            Assert.ArgumentNotNull((object)context, "context");
            Assert.ArgumentNotNull((object)indexable, "indexable");
            this.Index.Locator.GetInstance<IEvent>().RaiseEvent("indexing:adding", (object)context.Index.Name, (object)indexable.UniqueId, (object)indexable.AbsolutePath);
            if (this.IsExcludedFromIndex(indexable, false))
                return;
            foreach (Language language in indexable.Item.Languages)
            {
                Item item;
                //using (new SitecoreCachesDisabler())
                item = indexable.Item.Database.GetItem(indexable.Item.ID, language, Sitecore.Data.Version.Latest);
                if (item == null)
                {
                    CrawlingLog.Log.Warn(string.Format("SitecoreItemCrawler : AddItem : Could not build document data {0} - Latest version could not be found. Skipping.", (object)indexable.Item.Uri), (Exception)null);
                }
                else
                {
                    Item[] versions = item.Versions.GetVersions(false);
                    if (versions.Length == 0)
                    {
                        var strategy = new DefaultLanguageFallbackStrategy();
                        var fallbackLanguage = strategy.GetFallbackLanguage(item.Language, item.Database, item.ID);
                        if (fallbackLanguage != null)
                        {
                            var fallbackItem = item.Database.GetItem(item.ID, fallbackLanguage, Sitecore.Data.Version.Latest);
                            if (fallbackItem != null)
                            {
                                var stubData = new ItemData(fallbackItem.InnerData.Definition, item.Language, fallbackItem.Version,
                                    fallbackItem.InnerData.Fields);
                                var stub = new StubItem(item.ID, stubData, item.Database) { OriginalLanguage = item.Language };
                                stub.RuntimeSettings.SaveAll = true;

                                SitecoreIndexableItem sitecoreIndexableItem = (SitecoreIndexableItem)stub;
                                IIndexableBuiltinFields indexableBuiltinFields = (IIndexableBuiltinFields)sitecoreIndexableItem;
                                indexableBuiltinFields.IsLatestVersion = true;
                                sitecoreIndexableItem.IndexFieldStorageValueFormatter =
                                    context.Index.Configuration.IndexFieldStorageValueFormatter;
                                this.Operations.Add((IIndexable)sitecoreIndexableItem, context, this.index.Configuration);
                            }
                        }
                    }
                    else
                    {
                        versions = item.Versions.GetVersions();
                        foreach (Item obj2 in versions)
                        {
                            SitecoreIndexableItem sitecoreIndexableItem = (SitecoreIndexableItem)item;
                            IIndexableBuiltinFields indexableBuiltinFields = (IIndexableBuiltinFields)sitecoreIndexableItem;
                            indexableBuiltinFields.IsLatestVersion = indexableBuiltinFields.Version == item.Version.Number;
                            sitecoreIndexableItem.IndexFieldStorageValueFormatter =
                                context.Index.Configuration.IndexFieldStorageValueFormatter;
                            this.Operations.Add((IIndexable)sitecoreIndexableItem, context, this.index.Configuration);
                        }
                    }
                }
            }
            this.Index.Locator.GetInstance<IEvent>().RaiseEvent("indexing:added", (object)context.Index.Name, (object)indexable.UniqueId, (object)indexable.AbsolutePath);
        }
    }
}