











#pragma warning disable 1591
#pragma warning disable 0108
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Team Development for Sitecore.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;   
using System.Collections.Generic;   
using System.Linq;
using System.Text;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Fields;
using Sitecore.Globalization;
using Sitecore.Data;



namespace Landmark.Web.Models
{

	public partial interface IGlassBase{
		
		[SitecoreId]
		Guid Id{ get; }

		[SitecoreInfo(SitecoreInfoType.Language)]
        Language Language{ get; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        int Version { get; }

		[SitecoreInfo(SitecoreInfoType.Name)]
		string Name { get; set; }
	}

	public abstract partial class GlassBase : IGlassBase{
		
		[SitecoreId]
		public virtual Guid Id{ get; private set;}

		[SitecoreInfo(SitecoreInfoType.Language)]
        public virtual Language Language{ get; private set; }

        [SitecoreInfo(SitecoreInfoType.Version)]
        public virtual int Version { get; private set; }

		[SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; private set; }

		[SitecoreInfo(SitecoreInfoType.Name)]
		public virtual string Name { get; set; }

		[SitecoreInfo(SitecoreInfoType.FullPath)]
		public virtual string Path { get; set; }
	}
}

namespace Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC
{


 	/// <summary>
	/// IDocument_Item Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Document Item</para>	
	/// <para>ID: 440c78f9-3de9-49fc-b846-ca918e18d8b4</para>	
	/// </summary>
	[SitecoreType(TemplateId=IDocument_ItemConstants.TemplateIdString )] //, Cachable = true
	public partial interface IDocument_Item : IGlassBase , global::Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC.IBase_Item
	{
								/// <summary>
					/// The Author field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 2531b20d-5947-4661-a7a0-53ba8c0fe9bb</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IDocument_ItemConstants.AuthorFieldName)]
					string Author  {get; set;}
			
				}


	public static partial class IDocument_ItemConstants{

			public const string TemplateIdString = "440c78f9-3de9-49fc-b846-ca918e18d8b4";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "Document Item";

					
			public static readonly ID AuthorFieldId = new ID("2531b20d-5947-4661-a7a0-53ba8c0fe9bb");
			public const string AuthorFieldName = "Author";
			
					
			public static readonly ID DescriptionFieldId = new ID("4a4c6388-4745-477b-a61d-900351614444");
			public const string DescriptionFieldName = "Description";
			
					
			public static readonly ID TitleFieldId = new ID("8fa39420-c203-4d59-8f86-8d48732e9959");
			public const string TitleFieldName = "Title";
			
			

	}

	
	/// <summary>
	/// Document_Item
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Document Item</para>	
	/// <para>ID: 440c78f9-3de9-49fc-b846-ca918e18d8b4</para>	
	/// </summary>
	[SitecoreType(TemplateId=IDocument_ItemConstants.TemplateIdString)] //, Cachable = true
	public partial class Document_Item  : GlassBase, IDocument_Item 
	{
	   
						/// <summary>
				/// The Author field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 2531b20d-5947-4661-a7a0-53ba8c0fe9bb</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IDocument_ItemConstants.AuthorFieldName)]
				public virtual string Author  {get; set;}
					
						/// <summary>
				/// The Description field.
				/// <para></para>
				/// <para>Field Type: Rich Text</para>		
				/// <para>Field ID: 4a4c6388-4745-477b-a61d-900351614444</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IDocument_ItemConstants.DescriptionFieldName)]
				public virtual string Description  {get; set;}
					
						/// <summary>
				/// The Title field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 8fa39420-c203-4d59-8f86-8d48732e9959</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IDocument_ItemConstants.TitleFieldName)]
				public virtual string Title  {get; set;}
					
			
	}
}
namespace Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC
{


 	/// <summary>
	/// IHome_Page Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Home Page</para>	
	/// <para>ID: 4c0ba879-87e2-4b01-856f-19d85bd3a7c9</para>	
	/// </summary>
	[SitecoreType(TemplateId=IHome_PageConstants.TemplateIdString )] //, Cachable = true
	public partial interface IHome_Page : IGlassBase , global::Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC.IBase_Item
	{
								/// <summary>
					/// The Call to Action Link field.
					/// <para></para>
					/// <para>Field Type: General Link</para>		
					/// <para>Field ID: 7ed6e19c-559c-49e5-bc97-270b222cdaef</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IHome_PageConstants.Call_To_Action_LinkFieldName)]
					Link Call_To_Action_Link  {get; set;}
			
								/// <summary>
					/// The Call to Action Text field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 23db2802-71b6-4d1e-a412-2d1dbe69ee79</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IHome_PageConstants.Call_To_Action_TextFieldName)]
					string Call_To_Action_Text  {get; set;}
			
								/// <summary>
					/// The Image field.
					/// <para></para>
					/// <para>Field Type: Image</para>		
					/// <para>Field ID: 4da238ba-a834-481d-8723-9f13f703b690</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IHome_PageConstants.ImageFieldName)]
					Image Image  {get; set;}
			
				}


	public static partial class IHome_PageConstants{

			public const string TemplateIdString = "4c0ba879-87e2-4b01-856f-19d85bd3a7c9";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "Home Page";

					
			public static readonly ID Call_To_Action_LinkFieldId = new ID("7ed6e19c-559c-49e5-bc97-270b222cdaef");
			public const string Call_To_Action_LinkFieldName = "Call to Action Link";
			
					
			public static readonly ID Call_To_Action_TextFieldId = new ID("23db2802-71b6-4d1e-a412-2d1dbe69ee79");
			public const string Call_To_Action_TextFieldName = "Call to Action Text";
			
					
			public static readonly ID ImageFieldId = new ID("4da238ba-a834-481d-8723-9f13f703b690");
			public const string ImageFieldName = "Image";
			
					
			public static readonly ID DescriptionFieldId = new ID("4a4c6388-4745-477b-a61d-900351614444");
			public const string DescriptionFieldName = "Description";
			
					
			public static readonly ID TitleFieldId = new ID("8fa39420-c203-4d59-8f86-8d48732e9959");
			public const string TitleFieldName = "Title";
			
			

	}

	
	/// <summary>
	/// Home_Page
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Home Page</para>	
	/// <para>ID: 4c0ba879-87e2-4b01-856f-19d85bd3a7c9</para>	
	/// </summary>
	[SitecoreType(TemplateId=IHome_PageConstants.TemplateIdString)] //, Cachable = true
	public partial class Home_Page  : GlassBase, IHome_Page 
	{
	   
						/// <summary>
				/// The Call to Action Link field.
				/// <para></para>
				/// <para>Field Type: General Link</para>		
				/// <para>Field ID: 7ed6e19c-559c-49e5-bc97-270b222cdaef</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IHome_PageConstants.Call_To_Action_LinkFieldName)]
				public virtual Link Call_To_Action_Link  {get; set;}
					
						/// <summary>
				/// The Call to Action Text field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 23db2802-71b6-4d1e-a412-2d1dbe69ee79</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IHome_PageConstants.Call_To_Action_TextFieldName)]
				public virtual string Call_To_Action_Text  {get; set;}
					
						/// <summary>
				/// The Image field.
				/// <para></para>
				/// <para>Field Type: Image</para>		
				/// <para>Field ID: 4da238ba-a834-481d-8723-9f13f703b690</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IHome_PageConstants.ImageFieldName)]
				public virtual Image Image  {get; set;}
					
						/// <summary>
				/// The Description field.
				/// <para></para>
				/// <para>Field Type: Rich Text</para>		
				/// <para>Field ID: 4a4c6388-4745-477b-a61d-900351614444</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IHome_PageConstants.DescriptionFieldName)]
				public virtual string Description  {get; set;}
					
						/// <summary>
				/// The Title field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 8fa39420-c203-4d59-8f86-8d48732e9959</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IHome_PageConstants.TitleFieldName)]
				public virtual string Title  {get; set;}
					
			
	}
}
namespace Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC
{


 	/// <summary>
	/// IBase_Item Interface
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Base Item</para>	
	/// <para>ID: e2145f3b-75a1-4622-b747-1a231771a499</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBase_ItemConstants.TemplateIdString )] //, Cachable = true
	public partial interface IBase_Item : IGlassBase 
	{
								/// <summary>
					/// The Description field.
					/// <para></para>
					/// <para>Field Type: Rich Text</para>		
					/// <para>Field ID: 4a4c6388-4745-477b-a61d-900351614444</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBase_ItemConstants.DescriptionFieldName)]
					string Description  {get; set;}
			
								/// <summary>
					/// The Title field.
					/// <para></para>
					/// <para>Field Type: Single-Line Text</para>		
					/// <para>Field ID: 8fa39420-c203-4d59-8f86-8d48732e9959</para>
					/// <para>Custom Data: </para>
					/// </summary>
					[SitecoreField(IBase_ItemConstants.TitleFieldName)]
					string Title  {get; set;}
			
				}


	public static partial class IBase_ItemConstants{

			public const string TemplateIdString = "e2145f3b-75a1-4622-b747-1a231771a499";
			public static readonly ID TemplateId = new ID(TemplateIdString);
			public const string TemplateName = "Base Item";

					
			public static readonly ID DescriptionFieldId = new ID("4a4c6388-4745-477b-a61d-900351614444");
			public const string DescriptionFieldName = "Description";
			
					
			public static readonly ID TitleFieldId = new ID("8fa39420-c203-4d59-8f86-8d48732e9959");
			public const string TitleFieldName = "Title";
			
			

	}

	
	/// <summary>
	/// Base_Item
	/// <para></para>
	/// <para>Path: /sitecore/templates/User Defined/MyFirstSitecoreMVC/Base Item</para>	
	/// <para>ID: e2145f3b-75a1-4622-b747-1a231771a499</para>	
	/// </summary>
	[SitecoreType(TemplateId=IBase_ItemConstants.TemplateIdString)] //, Cachable = true
	public partial class Base_Item  : GlassBase, IBase_Item 
	{
	   
						/// <summary>
				/// The Description field.
				/// <para></para>
				/// <para>Field Type: Rich Text</para>		
				/// <para>Field ID: 4a4c6388-4745-477b-a61d-900351614444</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBase_ItemConstants.DescriptionFieldName)]
				public virtual string Description  {get; set;}
					
						/// <summary>
				/// The Title field.
				/// <para></para>
				/// <para>Field Type: Single-Line Text</para>		
				/// <para>Field ID: 8fa39420-c203-4d59-8f86-8d48732e9959</para>
				/// <para>Custom Data: </para>
				/// </summary>
				[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Team Development for Sitecore - GlassItem.tt", "1.0")]
				[SitecoreField(IBase_ItemConstants.TitleFieldName)]
				public virtual string Title  {get; set;}
					
			
	}
}
