﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdateDataSimple
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DatabaseDataEntryBPO")]
	public partial class DataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Inserttbl_Version(tbl_Version instance);
    partial void Updatetbl_Version(tbl_Version instance);
    partial void Deletetbl_Version(tbl_Version instance);
    #endregion
		
		public DataDataContext() : 
				base(global::UpdateDataSimple.Properties.Settings.Default.DatabaseDataEntryBPOConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tbl_Version> tbl_Versions
		{
			get
			{
				return this.GetTable<tbl_Version>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tbl_Version")]
	public partial class tbl_Version : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _IDProject;
		
		private string _IDVersion;
		
		private int _ID_int_auto;
		
		private string _MoTaChucNangMoi;
		
		private System.Nullable<bool> _OutSource;
		
		private System.Nullable<bool> _Is_Update;
		
		private string _LinkUpdate;
		
		private string _FileName_Update;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDProjectChanging(string value);
    partial void OnIDProjectChanged();
    partial void OnIDVersionChanging(string value);
    partial void OnIDVersionChanged();
    partial void OnID_int_autoChanging(int value);
    partial void OnID_int_autoChanged();
    partial void OnMoTaChucNangMoiChanging(string value);
    partial void OnMoTaChucNangMoiChanged();
    partial void OnOutSourceChanging(System.Nullable<bool> value);
    partial void OnOutSourceChanged();
    partial void OnIs_UpdateChanging(System.Nullable<bool> value);
    partial void OnIs_UpdateChanged();
    partial void OnLinkUpdateChanging(string value);
    partial void OnLinkUpdateChanged();
    partial void OnFileName_UpdateChanging(string value);
    partial void OnFileName_UpdateChanged();
    #endregion
		
		public tbl_Version()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IDProject", DbType="NVarChar(150) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string IDProject
		{
			get
			{
				return this._IDProject;
			}
			set
			{
				if ((this._IDProject != value))
				{
					this.OnIDProjectChanging(value);
					this.SendPropertyChanging();
					this._IDProject = value;
					this.SendPropertyChanged("IDProject");
					this.OnIDProjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IDVersion", DbType="NVarChar(100)")]
		public string IDVersion
		{
			get
			{
				return this._IDVersion;
			}
			set
			{
				if ((this._IDVersion != value))
				{
					this.OnIDVersionChanging(value);
					this.SendPropertyChanging();
					this._IDVersion = value;
					this.SendPropertyChanged("IDVersion");
					this.OnIDVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_int_auto", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ID_int_auto
		{
			get
			{
				return this._ID_int_auto;
			}
			set
			{
				if ((this._ID_int_auto != value))
				{
					this.OnID_int_autoChanging(value);
					this.SendPropertyChanging();
					this._ID_int_auto = value;
					this.SendPropertyChanged("ID_int_auto");
					this.OnID_int_autoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MoTaChucNangMoi", DbType="NVarChar(200)")]
		public string MoTaChucNangMoi
		{
			get
			{
				return this._MoTaChucNangMoi;
			}
			set
			{
				if ((this._MoTaChucNangMoi != value))
				{
					this.OnMoTaChucNangMoiChanging(value);
					this.SendPropertyChanging();
					this._MoTaChucNangMoi = value;
					this.SendPropertyChanged("MoTaChucNangMoi");
					this.OnMoTaChucNangMoiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OutSource", DbType="Bit")]
		public System.Nullable<bool> OutSource
		{
			get
			{
				return this._OutSource;
			}
			set
			{
				if ((this._OutSource != value))
				{
					this.OnOutSourceChanging(value);
					this.SendPropertyChanging();
					this._OutSource = value;
					this.SendPropertyChanged("OutSource");
					this.OnOutSourceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Is_Update", DbType="Bit")]
		public System.Nullable<bool> Is_Update
		{
			get
			{
				return this._Is_Update;
			}
			set
			{
				if ((this._Is_Update != value))
				{
					this.OnIs_UpdateChanging(value);
					this.SendPropertyChanging();
					this._Is_Update = value;
					this.SendPropertyChanged("Is_Update");
					this.OnIs_UpdateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LinkUpdate", DbType="NVarChar(255)")]
		public string LinkUpdate
		{
			get
			{
				return this._LinkUpdate;
			}
			set
			{
				if ((this._LinkUpdate != value))
				{
					this.OnLinkUpdateChanging(value);
					this.SendPropertyChanging();
					this._LinkUpdate = value;
					this.SendPropertyChanged("LinkUpdate");
					this.OnLinkUpdateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName_Update", DbType="NVarChar(255)")]
		public string FileName_Update
		{
			get
			{
				return this._FileName_Update;
			}
			set
			{
				if ((this._FileName_Update != value))
				{
					this.OnFileName_UpdateChanging(value);
					this.SendPropertyChanging();
					this._FileName_Update = value;
					this.SendPropertyChanged("FileName_Update");
					this.OnFileName_UpdateChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
