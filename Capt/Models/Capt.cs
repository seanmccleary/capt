// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from Capt on 2011-10-09 18:23:25Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
namespace Capt.Models
{
	using System;
	using System.ComponentModel;
	using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class Capt : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public Capt(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public Capt(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Capt(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<Caption> Captions
		{
			get
			{
				return this.GetTable<Caption>();
			}
		}
		
		public Table<CaptionComment> CaptionComments
		{
			get
			{
				return this.GetTable<CaptionComment>();
			}
		}
		
		public Table<CaptionVote> CaptionVotes
		{
			get
			{
				return this.GetTable<CaptionVote>();
			}
		}
		
		public Table<Comment> Comments
		{
			get
			{
				return this.GetTable<Comment>();
			}
		}
		
		public Table<Event> Events
		{
			get
			{
				return this.GetTable<Event>();
			}
		}
		
		public Table<EventType> EventTypes
		{
			get
			{
				return this.GetTable<EventType>();
			}
		}
		
		public Table<ExternalLogin> ExternalLogins
		{
			get
			{
				return this.GetTable<ExternalLogin>();
			}
		}
		
		public Table<ExternalLoginProvider> ExternalLoginProviders
		{
			get
			{
				return this.GetTable<ExternalLoginProvider>();
			}
		}
		
		public Table<License> Licenses
		{
			get
			{
				return this.GetTable<License>();
			}
		}
		
		public Table<OAuthToken> OAuthTokens
		{
			get
			{
				return this.GetTable<OAuthToken>();
			}
		}
		
		public Table<Picture> Pictures
		{
			get
			{
				return this.GetTable<Picture>();
			}
		}
		
		public Table<PictureComment> PictureComments
		{
			get
			{
				return this.GetTable<PictureComment>();
			}
		}
		
		public Table<PictureVote> PictureVotes
		{
			get
			{
				return this.GetTable<PictureVote>();
			}
		}
		
		public Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public Table<Vote> Votes
		{
			get
			{
				return this.GetTable<Vote>();
			}
		}
	}
	
	#region Start MONO_STRICT
#if MONO_STRICT

	public partial class Capt
	{
		
		public Capt(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
	}
	#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT
	
	public partial class Capt
	{
		
		public Capt(IDbConnection connection) : 
				base(connection, new DbLinq.MySql.MySqlVendor())
		{
			this.OnCreated();
		}
		
		public Capt(IDbConnection connection, IVendor sqlDialect) : 
				base(connection, sqlDialect)
		{
			this.OnCreated();
		}
		
		public Capt(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
				base(connection, mappingSource, sqlDialect)
		{
			this.OnCreated();
		}
	}
	#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
	#endregion
	
	[Table(Name="Caption")]
	public partial class Caption : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _adminNote;
		
		private int _createEventID;
		
		private int _id;
		
		private bool _isAnonymous;
		
		private bool _isVisible;
		
		private int _pictureID;
		
		private string _text;
		
		private int _userID;
		
		private EntitySet<CaptionComment> _captionComments;
		
		private EntitySet<CaptionVote> _captionVotes;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		private EntityRef<Picture> _picture = new EntityRef<Picture>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAdminNoteChanged();
		
		partial void OnAdminNoteChanging(string value);
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIsAnonymousChanged();
		
		partial void OnIsAnonymousChanging(bool value);
		
		partial void OnIsVisibleChanged();
		
		partial void OnIsVisibleChanging(bool value);
		
		partial void OnPictureIdChanged();
		
		partial void OnPictureIdChanging(int value);
		
		partial void OnTextChanged();
		
		partial void OnTextChanging(string value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(int value);
		#endregion
		
		
		public Caption()
		{
			_captionComments = new EntitySet<CaptionComment>(new Action<CaptionComment>(this.CaptionComments_Attach), new Action<CaptionComment>(this.CaptionComments_Detach));
			_captionVotes = new EntitySet<CaptionVote>(new Action<CaptionVote>(this.CaptionVotes_Attach), new Action<CaptionVote>(this.CaptionVotes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_adminNote", Name="AdminNote", DbType="varchar(140)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdminNote
		{
			get
			{
				return this._adminNote;
			}
			set
			{
				if (((_adminNote == value) 
							== false))
				{
					this.OnAdminNoteChanging(value);
					this.SendPropertyChanging();
					this._adminNote = value;
					this.SendPropertyChanged("AdminNote");
					this.OnAdminNoteChanged();
				}
			}
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_isAnonymous", Name="IsAnonymous", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsAnonymous
		{
			get
			{
				return this._isAnonymous;
			}
			set
			{
				if ((_isAnonymous != value))
				{
					this.OnIsAnonymousChanging(value);
					this.SendPropertyChanging();
					this._isAnonymous = value;
					this.SendPropertyChanged("IsAnonymous");
					this.OnIsAnonymousChanged();
				}
			}
		}
		
		[Column(Storage="_isVisible", Name="IsVisible", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if ((_isVisible != value))
				{
					this.OnIsVisibleChanging(value);
					this.SendPropertyChanging();
					this._isVisible = value;
					this.SendPropertyChanged("IsVisible");
					this.OnIsVisibleChanged();
				}
			}
		}
		
		[Column(Storage="_pictureID", Name="PictureId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int PictureId
		{
			get
			{
				return this._pictureID;
			}
			set
			{
				if ((_pictureID != value))
				{
					if (_picture.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPictureIdChanging(value);
					this.SendPropertyChanging();
					this._pictureID = value;
					this.SendPropertyChanged("PictureId");
					this.OnPictureIdChanged();
				}
			}
		}
		
		[Column(Storage="_text", Name="Text", DbType="varchar(140)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (((_text == value) 
							== false))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_captionComments", OtherKey="CaptionId", ThisKey="Id", Name="fk_Caption_has_Comment_Caption1")]
		[DebuggerNonUserCode()]
		public EntitySet<CaptionComment> CaptionComments
		{
			get
			{
				return this._captionComments;
			}
			set
			{
				this._captionComments = value;
			}
		}
		
		[Association(Storage="_captionVotes", OtherKey="CaptionId", ThisKey="Id", Name="fk_Vote_has_Caption_Caption1")]
		[DebuggerNonUserCode()]
		public EntitySet<CaptionVote> CaptionVotes
		{
			get
			{
				return this._captionVotes;
			}
			set
			{
				this._captionVotes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_Caption_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.Captions.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.Captions.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_picture", OtherKey="Id", ThisKey="PictureId", Name="fk_Caption_Picture1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Picture Picture
		{
			get
			{
				return this._picture.Entity;
			}
			set
			{
				if (((this._picture.Entity == value) 
							== false))
				{
					if ((this._picture.Entity != null))
					{
						Picture previousPicture = this._picture.Entity;
						this._picture.Entity = null;
						previousPicture.Captions.Remove(this);
					}
					this._picture.Entity = value;
					if ((value != null))
					{
						value.Captions.Add(this);
						_pictureID = value.Id;
					}
					else
					{
						_pictureID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_Caption_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.Captions.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.Captions.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void CaptionComments_Attach(CaptionComment entity)
		{
			this.SendPropertyChanging();
			entity.Caption = this;
		}
		
		private void CaptionComments_Detach(CaptionComment entity)
		{
			this.SendPropertyChanging();
			entity.Caption = null;
		}
		
		private void CaptionVotes_Attach(CaptionVote entity)
		{
			this.SendPropertyChanging();
			entity.Caption = this;
		}
		
		private void CaptionVotes_Detach(CaptionVote entity)
		{
			this.SendPropertyChanging();
			entity.Caption = null;
		}
		#endregion
	}
	
	[Table(Name="CaptionComment")]
	public partial class CaptionComment : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _captionID;
		
		private int _commentID;
		
		private EntityRef<Caption> _caption = new EntityRef<Caption>();
		
		private EntityRef<Comment> _comment = new EntityRef<Comment>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCaptionIdChanged();
		
		partial void OnCaptionIdChanging(int value);
		
		partial void OnCommentIdChanged();
		
		partial void OnCommentIdChanging(int value);
		#endregion
		
		
		public CaptionComment()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_captionID", Name="CaptionId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CaptionId
		{
			get
			{
				return this._captionID;
			}
			set
			{
				if ((_captionID != value))
				{
					if (_caption.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCaptionIdChanging(value);
					this.SendPropertyChanging();
					this._captionID = value;
					this.SendPropertyChanged("CaptionId");
					this.OnCaptionIdChanged();
				}
			}
		}
		
		[Column(Storage="_commentID", Name="CommentId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CommentId
		{
			get
			{
				return this._commentID;
			}
			set
			{
				if ((_commentID != value))
				{
					if (_comment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCommentIdChanging(value);
					this.SendPropertyChanging();
					this._commentID = value;
					this.SendPropertyChanged("CommentId");
					this.OnCommentIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_caption", OtherKey="Id", ThisKey="CaptionId", Name="fk_Caption_has_Comment_Caption1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Caption Caption
		{
			get
			{
				return this._caption.Entity;
			}
			set
			{
				if (((this._caption.Entity == value) 
							== false))
				{
					if ((this._caption.Entity != null))
					{
						Caption previousCaption = this._caption.Entity;
						this._caption.Entity = null;
						previousCaption.CaptionComments.Remove(this);
					}
					this._caption.Entity = value;
					if ((value != null))
					{
						value.CaptionComments.Add(this);
						_captionID = value.Id;
					}
					else
					{
						_captionID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_comment", OtherKey="Id", ThisKey="CommentId", Name="fk_Caption_has_Comment_Comment1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Comment Comment
		{
			get
			{
				return this._comment.Entity;
			}
			set
			{
				if (((this._comment.Entity == value) 
							== false))
				{
					if ((this._comment.Entity != null))
					{
						Comment previousComment = this._comment.Entity;
						this._comment.Entity = null;
						previousComment.CaptionComments.Remove(this);
					}
					this._comment.Entity = value;
					if ((value != null))
					{
						value.CaptionComments.Add(this);
						_commentID = value.Id;
					}
					else
					{
						_commentID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="CaptionVote")]
	public partial class CaptionVote : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _captionID;
		
		private int _voteID;
		
		private EntityRef<Caption> _caption = new EntityRef<Caption>();
		
		private EntityRef<Vote> _vote = new EntityRef<Vote>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCaptionIdChanged();
		
		partial void OnCaptionIdChanging(int value);
		
		partial void OnVoteIdChanged();
		
		partial void OnVoteIdChanging(int value);
		#endregion
		
		
		public CaptionVote()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_captionID", Name="CaptionId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CaptionId
		{
			get
			{
				return this._captionID;
			}
			set
			{
				if ((_captionID != value))
				{
					if (_caption.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCaptionIdChanging(value);
					this.SendPropertyChanging();
					this._captionID = value;
					this.SendPropertyChanged("CaptionId");
					this.OnCaptionIdChanged();
				}
			}
		}
		
		[Column(Storage="_voteID", Name="VoteId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int VoteId
		{
			get
			{
				return this._voteID;
			}
			set
			{
				if ((_voteID != value))
				{
					if (_vote.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVoteIdChanging(value);
					this.SendPropertyChanging();
					this._voteID = value;
					this.SendPropertyChanged("VoteId");
					this.OnVoteIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_caption", OtherKey="Id", ThisKey="CaptionId", Name="fk_Vote_has_Caption_Caption1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Caption Caption
		{
			get
			{
				return this._caption.Entity;
			}
			set
			{
				if (((this._caption.Entity == value) 
							== false))
				{
					if ((this._caption.Entity != null))
					{
						Caption previousCaption = this._caption.Entity;
						this._caption.Entity = null;
						previousCaption.CaptionVotes.Remove(this);
					}
					this._caption.Entity = value;
					if ((value != null))
					{
						value.CaptionVotes.Add(this);
						_captionID = value.Id;
					}
					else
					{
						_captionID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_vote", OtherKey="Id", ThisKey="VoteId", Name="fk_Vote_has_Caption_Vote1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Vote Vote
		{
			get
			{
				return this._vote.Entity;
			}
			set
			{
				if (((this._vote.Entity == value) 
							== false))
				{
					if ((this._vote.Entity != null))
					{
						Vote previousVote = this._vote.Entity;
						this._vote.Entity = null;
						previousVote.CaptionVotes.Remove(this);
					}
					this._vote.Entity = value;
					if ((value != null))
					{
						value.CaptionVotes.Add(this);
						_voteID = value.Id;
					}
					else
					{
						_voteID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="Comment")]
	public partial class Comment : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _adminNote;
		
		private int _createEventID;
		
		private int _id;
		
		private bool _isVisible;
		
		private System.Nullable<int> _parentCommentID;
		
		private string _text;
		
		private int _userID;
		
		private EntitySet<CaptionComment> _captionComments;
		
		private EntitySet<Comment> _comments;
		
		private EntitySet<PictureComment> _pictureComments;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		private EntityRef<Comment> _parentCommentIDComment = new EntityRef<Comment>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAdminNoteChanged();
		
		partial void OnAdminNoteChanging(string value);
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIsVisibleChanged();
		
		partial void OnIsVisibleChanging(bool value);
		
		partial void OnParentCommentIdChanged();
		
		partial void OnParentCommentIdChanging(System.Nullable<int> value);
		
		partial void OnTextChanged();
		
		partial void OnTextChanging(string value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(int value);
		#endregion
		
		
		public Comment()
		{
			_captionComments = new EntitySet<CaptionComment>(new Action<CaptionComment>(this.CaptionComments_Attach), new Action<CaptionComment>(this.CaptionComments_Detach));
			_comments = new EntitySet<Comment>(new Action<Comment>(this.Comments_Attach), new Action<Comment>(this.Comments_Detach));
			_pictureComments = new EntitySet<PictureComment>(new Action<PictureComment>(this.PictureComments_Attach), new Action<PictureComment>(this.PictureComments_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_adminNote", Name="AdminNote", DbType="varchar(140)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdminNote
		{
			get
			{
				return this._adminNote;
			}
			set
			{
				if (((_adminNote == value) 
							== false))
				{
					this.OnAdminNoteChanging(value);
					this.SendPropertyChanging();
					this._adminNote = value;
					this.SendPropertyChanged("AdminNote");
					this.OnAdminNoteChanged();
				}
			}
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_isVisible", Name="IsVisible", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if ((_isVisible != value))
				{
					this.OnIsVisibleChanging(value);
					this.SendPropertyChanging();
					this._isVisible = value;
					this.SendPropertyChanged("IsVisible");
					this.OnIsVisibleChanged();
				}
			}
		}
		
		[Column(Storage="_parentCommentID", Name="ParentCommentId", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> ParentCommentId
		{
			get
			{
				return this._parentCommentID;
			}
			set
			{
				if ((_parentCommentID != value))
				{
					if (_parentCommentIDComment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParentCommentIdChanging(value);
					this.SendPropertyChanging();
					this._parentCommentID = value;
					this.SendPropertyChanged("ParentCommentId");
					this.OnParentCommentIdChanged();
				}
			}
		}
		
		[Column(Storage="_text", Name="Text", DbType="varchar(140)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (((_text == value) 
							== false))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_captionComments", OtherKey="CommentId", ThisKey="Id", Name="fk_Caption_has_Comment_Comment1")]
		[DebuggerNonUserCode()]
		public EntitySet<CaptionComment> CaptionComments
		{
			get
			{
				return this._captionComments;
			}
			set
			{
				this._captionComments = value;
			}
		}
		
		[Association(Storage="_comments", OtherKey="ParentCommentId", ThisKey="Id", Name="FK_ParentCommentId")]
		[DebuggerNonUserCode()]
		public EntitySet<Comment> Comments
		{
			get
			{
				return this._comments;
			}
			set
			{
				this._comments = value;
			}
		}
		
		[Association(Storage="_pictureComments", OtherKey="CommentId", ThisKey="Id", Name="fk_Picture_has_Comment_Comment1")]
		[DebuggerNonUserCode()]
		public EntitySet<PictureComment> PictureComments
		{
			get
			{
				return this._pictureComments;
			}
			set
			{
				this._pictureComments = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_Comment_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.Comments.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.Comments.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_Comment_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.Comments.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.Comments.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_parentCommentIDComment", OtherKey="Id", ThisKey="ParentCommentId", Name="FK_ParentCommentId", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Comment ParentCommentIdComment
		{
			get
			{
				return this._parentCommentIDComment.Entity;
			}
			set
			{
				if (((this._parentCommentIDComment.Entity == value) 
							== false))
				{
					if ((this._parentCommentIDComment.Entity != null))
					{
						Comment previousComment = this._parentCommentIDComment.Entity;
						this._parentCommentIDComment.Entity = null;
						previousComment.Comments.Remove(this);
					}
					this._parentCommentIDComment.Entity = value;
					if ((value != null))
					{
						value.Comments.Add(this);
						_parentCommentID = value.Id;
					}
					else
					{
						_parentCommentID = null;
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void CaptionComments_Attach(CaptionComment entity)
		{
			this.SendPropertyChanging();
			entity.Comment = this;
		}
		
		private void CaptionComments_Detach(CaptionComment entity)
		{
			this.SendPropertyChanging();
			entity.Comment = null;
		}
		
		private void Comments_Attach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.ParentCommentIdComment = this;
		}
		
		private void Comments_Detach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.ParentCommentIdComment = null;
		}
		
		private void PictureComments_Attach(PictureComment entity)
		{
			this.SendPropertyChanging();
			entity.Comment = this;
		}
		
		private void PictureComments_Detach(PictureComment entity)
		{
			this.SendPropertyChanging();
			entity.Comment = null;
		}
		#endregion
	}
	
	[Table(Name="Event")]
	public partial class Event : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.DateTime _datetime;
		
		private int _eventTypeID;
		
		private string _host;
		
		private int _id;
		
		private int _ipV4aDdress;
		
		private EntitySet<Caption> _captions;
		
		private EntitySet<Comment> _comments;
		
		private EntitySet<ExternalLogin> _externalLogins;
		
		private EntitySet<OAuthToken> _oaUthTokens;
		
		private EntitySet<Picture> _pictures;
		
		private EntitySet<User> _users;
		
		private EntitySet<Vote> _votes;
		
		private EntityRef<EventType> _eventType = new EntityRef<EventType>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDatetimeChanged();
		
		partial void OnDatetimeChanging(System.DateTime value);
		
		partial void OnEventTypeIdChanged();
		
		partial void OnEventTypeIdChanging(int value);
		
		partial void OnHostChanged();
		
		partial void OnHostChanging(string value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIPv4AddressChanged();
		
		partial void OnIPv4AddressChanging(int value);
		#endregion
		
		
		public Event()
		{
			_captions = new EntitySet<Caption>(new Action<Caption>(this.Captions_Attach), new Action<Caption>(this.Captions_Detach));
			_comments = new EntitySet<Comment>(new Action<Comment>(this.Comments_Attach), new Action<Comment>(this.Comments_Detach));
			_externalLogins = new EntitySet<ExternalLogin>(new Action<ExternalLogin>(this.ExternalLogins_Attach), new Action<ExternalLogin>(this.ExternalLogins_Detach));
			_oaUthTokens = new EntitySet<OAuthToken>(new Action<OAuthToken>(this.OAuthTokens_Attach), new Action<OAuthToken>(this.OAuthTokens_Detach));
			_pictures = new EntitySet<Picture>(new Action<Picture>(this.Pictures_Attach), new Action<Picture>(this.Pictures_Detach));
			_users = new EntitySet<User>(new Action<User>(this.Users_Attach), new Action<User>(this.Users_Detach));
			_votes = new EntitySet<Vote>(new Action<Vote>(this.Votes_Attach), new Action<Vote>(this.Votes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_datetime", Name="Datetime", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Datetime
		{
			get
			{
				return this._datetime;
			}
			set
			{
				if ((_datetime != value))
				{
					this.OnDatetimeChanging(value);
					this.SendPropertyChanging();
					this._datetime = value;
					this.SendPropertyChanged("Datetime");
					this.OnDatetimeChanged();
				}
			}
		}
		
		[Column(Storage="_eventTypeID", Name="EventTypeId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int EventTypeId
		{
			get
			{
				return this._eventTypeID;
			}
			set
			{
				if ((_eventTypeID != value))
				{
					if (_eventType.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnEventTypeIdChanging(value);
					this.SendPropertyChanging();
					this._eventTypeID = value;
					this.SendPropertyChanged("EventTypeId");
					this.OnEventTypeIdChanged();
				}
			}
		}
		
		[Column(Storage="_host", Name="Host", DbType="varchar(100)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (((_host == value) 
							== false))
				{
					this.OnHostChanging(value);
					this.SendPropertyChanging();
					this._host = value;
					this.SendPropertyChanged("Host");
					this.OnHostChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_ipV4aDdress", Name="IPv4Address", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int IPv4Address
		{
			get
			{
				return this._ipV4aDdress;
			}
			set
			{
				if ((_ipV4aDdress != value))
				{
					this.OnIPv4AddressChanging(value);
					this.SendPropertyChanging();
					this._ipV4aDdress = value;
					this.SendPropertyChanged("IPv4Address");
					this.OnIPv4AddressChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_captions", OtherKey="CreateEventId", ThisKey="Id", Name="fk_Caption_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<Caption> Captions
		{
			get
			{
				return this._captions;
			}
			set
			{
				this._captions = value;
			}
		}
		
		[Association(Storage="_comments", OtherKey="CreateEventId", ThisKey="Id", Name="fk_Comment_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<Comment> Comments
		{
			get
			{
				return this._comments;
			}
			set
			{
				this._comments = value;
			}
		}
		
		[Association(Storage="_externalLogins", OtherKey="CreateEventId", ThisKey="Id", Name="fk_ExternalLogin_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<ExternalLogin> ExternalLogins
		{
			get
			{
				return this._externalLogins;
			}
			set
			{
				this._externalLogins = value;
			}
		}
		
		[Association(Storage="_oaUthTokens", OtherKey="CreateEventId", ThisKey="Id", Name="fk_OAuthToken_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<OAuthToken> OAuthTokens
		{
			get
			{
				return this._oaUthTokens;
			}
			set
			{
				this._oaUthTokens = value;
			}
		}
		
		[Association(Storage="_pictures", OtherKey="CreateEventId", ThisKey="Id", Name="fk_Picture_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<Picture> Pictures
		{
			get
			{
				return this._pictures;
			}
			set
			{
				this._pictures = value;
			}
		}
		
		[Association(Storage="_users", OtherKey="CreateEventId", ThisKey="Id", Name="fk_User_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<User> Users
		{
			get
			{
				return this._users;
			}
			set
			{
				this._users = value;
			}
		}
		
		[Association(Storage="_votes", OtherKey="CreateEventId", ThisKey="Id", Name="fk_Vote_Event1")]
		[DebuggerNonUserCode()]
		public EntitySet<Vote> Votes
		{
			get
			{
				return this._votes;
			}
			set
			{
				this._votes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_eventType", OtherKey="ID", ThisKey="EventTypeId", Name="fk_Event_EventType1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public EventType EventType
		{
			get
			{
				return this._eventType.Entity;
			}
			set
			{
				if (((this._eventType.Entity == value) 
							== false))
				{
					if ((this._eventType.Entity != null))
					{
						EventType previousEventType = this._eventType.Entity;
						this._eventType.Entity = null;
						previousEventType.Events.Remove(this);
					}
					this._eventType.Entity = value;
					if ((value != null))
					{
						value.Events.Add(this);
						_eventTypeID = value.ID;
					}
					else
					{
						_eventTypeID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Captions_Attach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void Captions_Detach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void Comments_Attach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void Comments_Detach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void ExternalLogins_Attach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void ExternalLogins_Detach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void OAuthTokens_Attach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void OAuthTokens_Detach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void Pictures_Attach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void Pictures_Detach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void Users_Attach(User entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void Users_Detach(User entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		
		private void Votes_Attach(Vote entity)
		{
			this.SendPropertyChanging();
			entity.Event = this;
		}
		
		private void Votes_Detach(Vote entity)
		{
			this.SendPropertyChanging();
			entity.Event = null;
		}
		#endregion
	}
	
	[Table(Name="EventType")]
	public partial class EventType : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _id;
		
		private EntitySet<Event> _events;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIDChanged();
		
		partial void OnIDChanging(int value);
		#endregion
		
		
		public EventType()
		{
			_events = new EntitySet<Event>(new Action<Event>(this.Events_Attach), new Action<Event>(this.Events_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="Description", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="ID", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_events", OtherKey="EventTypeId", ThisKey="ID", Name="fk_Event_EventType1")]
		[DebuggerNonUserCode()]
		public EntitySet<Event> Events
		{
			get
			{
				return this._events;
			}
			set
			{
				this._events = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Events_Attach(Event entity)
		{
			this.SendPropertyChanging();
			entity.EventType = this;
		}
		
		private void Events_Detach(Event entity)
		{
			this.SendPropertyChanging();
			entity.EventType = null;
		}
		#endregion
	}
	
	[Table(Name="ExternalLogin")]
	public partial class ExternalLogin : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _createEventID;
		
		private int _externalLoginProviderID;
		
		private int _id;
		
		private string _identifier;
		
		private int _userID;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		private EntityRef<ExternalLoginProvider> _externalLoginProvider = new EntityRef<ExternalLoginProvider>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnExternalLoginProviderIdChanged();
		
		partial void OnExternalLoginProviderIdChanging(int value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIdentifierChanged();
		
		partial void OnIdentifierChanging(string value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(int value);
		#endregion
		
		
		public ExternalLogin()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_externalLoginProviderID", Name="ExternalLoginProviderId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ExternalLoginProviderId
		{
			get
			{
				return this._externalLoginProviderID;
			}
			set
			{
				if ((_externalLoginProviderID != value))
				{
					if (_externalLoginProvider.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnExternalLoginProviderIdChanging(value);
					this.SendPropertyChanging();
					this._externalLoginProviderID = value;
					this.SendPropertyChanged("ExternalLoginProviderId");
					this.OnExternalLoginProviderIdChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_identifier", Name="Identifier", DbType="varchar(767)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				if (((_identifier == value) 
							== false))
				{
					this.OnIdentifierChanging(value);
					this.SendPropertyChanging();
					this._identifier = value;
					this.SendPropertyChanged("Identifier");
					this.OnIdentifierChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_ExternalLogin_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.ExternalLogins.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.ExternalLogins.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_externalLoginProvider", OtherKey="Id", ThisKey="ExternalLoginProviderId", Name="fk_ExternalLogin_ExternalLoginProvider1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ExternalLoginProvider ExternalLoginProvider
		{
			get
			{
				return this._externalLoginProvider.Entity;
			}
			set
			{
				if (((this._externalLoginProvider.Entity == value) 
							== false))
				{
					if ((this._externalLoginProvider.Entity != null))
					{
						ExternalLoginProvider previousExternalLoginProvider = this._externalLoginProvider.Entity;
						this._externalLoginProvider.Entity = null;
						previousExternalLoginProvider.ExternalLogins.Remove(this);
					}
					this._externalLoginProvider.Entity = value;
					if ((value != null))
					{
						value.ExternalLogins.Add(this);
						_externalLoginProviderID = value.Id;
					}
					else
					{
						_externalLoginProviderID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_ExternalLogin_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.ExternalLogins.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.ExternalLogins.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="ExternalLoginProvider")]
	public partial class ExternalLoginProvider : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _id;
		
		private string _name;
		
		private EntitySet<ExternalLogin> _externalLogins;
		
		private EntitySet<OAuthToken> _oaUthTokens;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
		
		
		public ExternalLoginProvider()
		{
			_externalLogins = new EntitySet<ExternalLogin>(new Action<ExternalLogin>(this.ExternalLogins_Attach), new Action<ExternalLogin>(this.ExternalLogins_Detach));
			_oaUthTokens = new EntitySet<OAuthToken>(new Action<OAuthToken>(this.OAuthTokens_Attach), new Action<OAuthToken>(this.OAuthTokens_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="varchar(45)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_externalLogins", OtherKey="ExternalLoginProviderId", ThisKey="Id", Name="fk_ExternalLogin_ExternalLoginProvider1")]
		[DebuggerNonUserCode()]
		public EntitySet<ExternalLogin> ExternalLogins
		{
			get
			{
				return this._externalLogins;
			}
			set
			{
				this._externalLogins = value;
			}
		}
		
		[Association(Storage="_oaUthTokens", OtherKey="ExternalLoginProviderId", ThisKey="Id", Name="fk_OauthTokens_ExternalLoginProvider1")]
		[DebuggerNonUserCode()]
		public EntitySet<OAuthToken> OAuthTokens
		{
			get
			{
				return this._oaUthTokens;
			}
			set
			{
				this._oaUthTokens = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void ExternalLogins_Attach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.ExternalLoginProvider = this;
		}
		
		private void ExternalLogins_Detach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.ExternalLoginProvider = null;
		}
		
		private void OAuthTokens_Attach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.ExternalLoginProvider = this;
		}
		
		private void OAuthTokens_Detach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.ExternalLoginProvider = null;
		}
		#endregion
	}
	
	[Table(Name="License")]
	public partial class License : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _description;
		
		private int _id;
		
		private string _imageUrl;
		
		private string _infoUrl;
		
		private EntitySet<Picture> _pictures;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnDescriptionChanged();
		
		partial void OnDescriptionChanging(string value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnImageUrlChanged();
		
		partial void OnImageUrlChanging(string value);
		
		partial void OnInfoUrlChanged();
		
		partial void OnInfoUrlChanging(string value);
		#endregion
		
		
		public License()
		{
			_pictures = new EntitySet<Picture>(new Action<Picture>(this.Pictures_Attach), new Action<Picture>(this.Pictures_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_description", Name="Description", DbType="varchar(100)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (((_description == value) 
							== false))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_imageUrl", Name="ImageUrl", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				if (((_imageUrl == value) 
							== false))
				{
					this.OnImageUrlChanging(value);
					this.SendPropertyChanging();
					this._imageUrl = value;
					this.SendPropertyChanged("ImageUrl");
					this.OnImageUrlChanged();
				}
			}
		}
		
		[Column(Storage="_infoUrl", Name="InfoUrl", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
			set
			{
				if (((_infoUrl == value) 
							== false))
				{
					this.OnInfoUrlChanging(value);
					this.SendPropertyChanging();
					this._infoUrl = value;
					this.SendPropertyChanged("InfoUrl");
					this.OnInfoUrlChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_pictures", OtherKey="LicenseId", ThisKey="Id", Name="fk_Picture_License1")]
		[DebuggerNonUserCode()]
		public EntitySet<Picture> Pictures
		{
			get
			{
				return this._pictures;
			}
			set
			{
				this._pictures = value;
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Pictures_Attach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.License = this;
		}
		
		private void Pictures_Detach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.License = null;
		}
		#endregion
	}
	
	[Table(Name="OAuthToken")]
	public partial class OAuthToken : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _createEventID;
		
		private System.Nullable<System.DateTime> _expires;
		
		private int _externalLoginProviderID;
		
		private int _id;
		
		private string _secret;
		
		private string _token;
		
		private int _userID;
		
		private EntityRef<ExternalLoginProvider> _externalLoginProvider = new EntityRef<ExternalLoginProvider>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnExpiresChanged();
		
		partial void OnExpiresChanging(System.Nullable<System.DateTime> value);
		
		partial void OnExternalLoginProviderIdChanged();
		
		partial void OnExternalLoginProviderIdChanging(int value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnSecretChanged();
		
		partial void OnSecretChanging(string value);
		
		partial void OnTokenChanged();
		
		partial void OnTokenChanging(string value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(int value);
		#endregion
		
		
		public OAuthToken()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_expires", Name="Expires", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> Expires
		{
			get
			{
				return this._expires;
			}
			set
			{
				if ((_expires != value))
				{
					this.OnExpiresChanging(value);
					this.SendPropertyChanging();
					this._expires = value;
					this.SendPropertyChanged("Expires");
					this.OnExpiresChanged();
				}
			}
		}
		
		[Column(Storage="_externalLoginProviderID", Name="ExternalLoginProviderId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ExternalLoginProviderId
		{
			get
			{
				return this._externalLoginProviderID;
			}
			set
			{
				if ((_externalLoginProviderID != value))
				{
					if (_externalLoginProvider.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnExternalLoginProviderIdChanging(value);
					this.SendPropertyChanging();
					this._externalLoginProviderID = value;
					this.SendPropertyChanged("ExternalLoginProviderId");
					this.OnExternalLoginProviderIdChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_secret", Name="Secret", DbType="varchar(1024)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Secret
		{
			get
			{
				return this._secret;
			}
			set
			{
				if (((_secret == value) 
							== false))
				{
					this.OnSecretChanging(value);
					this.SendPropertyChanging();
					this._secret = value;
					this.SendPropertyChanged("Secret");
					this.OnSecretChanged();
				}
			}
		}
		
		[Column(Storage="_token", Name="Token", DbType="varchar(1024)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Token
		{
			get
			{
				return this._token;
			}
			set
			{
				if (((_token == value) 
							== false))
				{
					this.OnTokenChanging(value);
					this.SendPropertyChanging();
					this._token = value;
					this.SendPropertyChanged("Token");
					this.OnTokenChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_externalLoginProvider", OtherKey="Id", ThisKey="ExternalLoginProviderId", Name="fk_OauthTokens_ExternalLoginProvider1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public ExternalLoginProvider ExternalLoginProvider
		{
			get
			{
				return this._externalLoginProvider.Entity;
			}
			set
			{
				if (((this._externalLoginProvider.Entity == value) 
							== false))
				{
					if ((this._externalLoginProvider.Entity != null))
					{
						ExternalLoginProvider previousExternalLoginProvider = this._externalLoginProvider.Entity;
						this._externalLoginProvider.Entity = null;
						previousExternalLoginProvider.OAuthTokens.Remove(this);
					}
					this._externalLoginProvider.Entity = value;
					if ((value != null))
					{
						value.OAuthTokens.Add(this);
						_externalLoginProviderID = value.Id;
					}
					else
					{
						_externalLoginProviderID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_OauthTokens_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.OAuthTokens.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.OAuthTokens.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_OAuthToken_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.OAuthTokens.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.OAuthTokens.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="Picture")]
	public partial class Picture : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.DateTime _activates;
		
		private string _adminNote;
		
		private string _attribution;
		
		private string _attributionUrl;
		
		private int _createEventID;
		
		private System.Guid _guid;
		
		private int _id;
		
		private bool _isNsfw;
		
		private bool _isPrivate;
		
		private bool _isVisible;
		
		private int _licenseID;
		
		private double _rank;
		
		private string _url;
		
		private System.Nullable<int> _userID;
		
		private EntitySet<Caption> _captions;
		
		private EntitySet<PictureComment> _pictureComments;
		
		private EntitySet<PictureVote> _pictureVotes;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		private EntityRef<License> _license = new EntityRef<License>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnActivatesChanged();
		
		partial void OnActivatesChanging(System.DateTime value);
		
		partial void OnAdminNoteChanged();
		
		partial void OnAdminNoteChanging(string value);
		
		partial void OnAttributionChanged();
		
		partial void OnAttributionChanging(string value);
		
		partial void OnAttributionUrlChanged();
		
		partial void OnAttributionUrlChanging(string value);
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnGuidChanged();
		
		partial void OnGuidChanging(System.Guid value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIsNSFWChanged();
		
		partial void OnIsNSFWChanging(bool value);
		
		partial void OnIsPrivateChanged();
		
		partial void OnIsPrivateChanging(bool value);
		
		partial void OnIsVisibleChanged();
		
		partial void OnIsVisibleChanging(bool value);
		
		partial void OnLicenseIdChanged();
		
		partial void OnLicenseIdChanging(int value);
		
		partial void OnRankChanged();
		
		partial void OnRankChanging(double value);
		
		partial void OnUrlChanged();
		
		partial void OnUrlChanging(string value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(System.Nullable<int> value);
		#endregion
		
		
		public Picture()
		{
			_captions = new EntitySet<Caption>(new Action<Caption>(this.Captions_Attach), new Action<Caption>(this.Captions_Detach));
			_pictureComments = new EntitySet<PictureComment>(new Action<PictureComment>(this.PictureComments_Attach), new Action<PictureComment>(this.PictureComments_Detach));
			_pictureVotes = new EntitySet<PictureVote>(new Action<PictureVote>(this.PictureVotes_Attach), new Action<PictureVote>(this.PictureVotes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_activates", Name="Activates", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime Activates
		{
			get
			{
				return this._activates;
			}
			set
			{
				if ((_activates != value))
				{
					this.OnActivatesChanging(value);
					this.SendPropertyChanging();
					this._activates = value;
					this.SendPropertyChanged("Activates");
					this.OnActivatesChanged();
				}
			}
		}
		
		[Column(Storage="_adminNote", Name="AdminNote", DbType="varchar(140)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdminNote
		{
			get
			{
				return this._adminNote;
			}
			set
			{
				if (((_adminNote == value) 
							== false))
				{
					this.OnAdminNoteChanging(value);
					this.SendPropertyChanging();
					this._adminNote = value;
					this.SendPropertyChanged("AdminNote");
					this.OnAdminNoteChanged();
				}
			}
		}
		
		[Column(Storage="_attribution", Name="Attribution", DbType="varchar(1024)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Attribution
		{
			get
			{
				return this._attribution;
			}
			set
			{
				if (((_attribution == value) 
							== false))
				{
					this.OnAttributionChanging(value);
					this.SendPropertyChanging();
					this._attribution = value;
					this.SendPropertyChanged("Attribution");
					this.OnAttributionChanged();
				}
			}
		}
		
		[Column(Storage="_attributionUrl", Name="AttributionUrl", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AttributionUrl
		{
			get
			{
				return this._attributionUrl;
			}
			set
			{
				if (((_attributionUrl == value) 
							== false))
				{
					this.OnAttributionUrlChanging(value);
					this.SendPropertyChanging();
					this._attributionUrl = value;
					this.SendPropertyChanged("AttributionUrl");
					this.OnAttributionUrlChanged();
				}
			}
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_guid", Name="Guid", DbType="char(36)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.Guid Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((_guid != value))
				{
					this.OnGuidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("Guid");
					this.OnGuidChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_isNsfw", Name="IsNSFW", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsNSFW
		{
			get
			{
				return this._isNsfw;
			}
			set
			{
				if ((_isNsfw != value))
				{
					this.OnIsNSFWChanging(value);
					this.SendPropertyChanging();
					this._isNsfw = value;
					this.SendPropertyChanged("IsNSFW");
					this.OnIsNSFWChanged();
				}
			}
		}
		
		[Column(Storage="_isPrivate", Name="IsPrivate", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsPrivate
		{
			get
			{
				return this._isPrivate;
			}
			set
			{
				if ((_isPrivate != value))
				{
					this.OnIsPrivateChanging(value);
					this.SendPropertyChanging();
					this._isPrivate = value;
					this.SendPropertyChanged("IsPrivate");
					this.OnIsPrivateChanged();
				}
			}
		}
		
		[Column(Storage="_isVisible", Name="IsVisible", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if ((_isVisible != value))
				{
					this.OnIsVisibleChanging(value);
					this.SendPropertyChanging();
					this._isVisible = value;
					this.SendPropertyChanged("IsVisible");
					this.OnIsVisibleChanged();
				}
			}
		}
		
		[Column(Storage="_licenseID", Name="LicenseId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LicenseId
		{
			get
			{
				return this._licenseID;
			}
			set
			{
				if ((_licenseID != value))
				{
					if (_license.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnLicenseIdChanging(value);
					this.SendPropertyChanging();
					this._licenseID = value;
					this.SendPropertyChanged("LicenseId");
					this.OnLicenseIdChanged();
				}
			}
		}
		
		[Column(Storage="_rank", Name="Rank", DbType="double", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public double Rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				if ((_rank != value))
				{
					this.OnRankChanging(value);
					this.SendPropertyChanging();
					this._rank = value;
					this.SendPropertyChanged("Rank");
					this.OnRankChanged();
				}
			}
		}
		
		[Column(Storage="_url", Name="Url", DbType="varchar(100)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public string Url
		{
			get
			{
				return this._url;
			}
			set
			{
				if (((_url == value) 
							== false))
				{
					this.OnUrlChanging(value);
					this.SendPropertyChanging();
					this._url = value;
					this.SendPropertyChanged("Url");
					this.OnUrlChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_captions", OtherKey="PictureId", ThisKey="Id", Name="fk_Caption_Picture1")]
		[DebuggerNonUserCode()]
		public EntitySet<Caption> Captions
		{
			get
			{
				return this._captions;
			}
			set
			{
				this._captions = value;
			}
		}
		
		[Association(Storage="_pictureComments", OtherKey="PictureId", ThisKey="Id", Name="fk_Picture_has_Comment_Picture1")]
		[DebuggerNonUserCode()]
		public EntitySet<PictureComment> PictureComments
		{
			get
			{
				return this._pictureComments;
			}
			set
			{
				this._pictureComments = value;
			}
		}
		
		[Association(Storage="_pictureVotes", OtherKey="PictureId", ThisKey="Id", Name="fk_Picture_has_Vote_Picture1")]
		[DebuggerNonUserCode()]
		public EntitySet<PictureVote> PictureVotes
		{
			get
			{
				return this._pictureVotes;
			}
			set
			{
				this._pictureVotes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_Picture_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.Pictures.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.Pictures.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_license", OtherKey="Id", ThisKey="LicenseId", Name="fk_Picture_License1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public License License
		{
			get
			{
				return this._license.Entity;
			}
			set
			{
				if (((this._license.Entity == value) 
							== false))
				{
					if ((this._license.Entity != null))
					{
						License previousLicense = this._license.Entity;
						this._license.Entity = null;
						previousLicense.Pictures.Remove(this);
					}
					this._license.Entity = value;
					if ((value != null))
					{
						value.Pictures.Add(this);
						_licenseID = value.Id;
					}
					else
					{
						_licenseID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_Picture_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.Pictures.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.Pictures.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = null;
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Captions_Attach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.Picture = this;
		}
		
		private void Captions_Detach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.Picture = null;
		}
		
		private void PictureComments_Attach(PictureComment entity)
		{
			this.SendPropertyChanging();
			entity.Picture = this;
		}
		
		private void PictureComments_Detach(PictureComment entity)
		{
			this.SendPropertyChanging();
			entity.Picture = null;
		}
		
		private void PictureVotes_Attach(PictureVote entity)
		{
			this.SendPropertyChanging();
			entity.Picture = this;
		}
		
		private void PictureVotes_Detach(PictureVote entity)
		{
			this.SendPropertyChanging();
			entity.Picture = null;
		}
		#endregion
	}
	
	[Table(Name="PictureComment")]
	public partial class PictureComment : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _commentID;
		
		private int _pictureID;
		
		private EntityRef<Comment> _comment = new EntityRef<Comment>();
		
		private EntityRef<Picture> _picture = new EntityRef<Picture>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnCommentIdChanged();
		
		partial void OnCommentIdChanging(int value);
		
		partial void OnPictureIdChanged();
		
		partial void OnPictureIdChanging(int value);
		#endregion
		
		
		public PictureComment()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_commentID", Name="CommentId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CommentId
		{
			get
			{
				return this._commentID;
			}
			set
			{
				if ((_commentID != value))
				{
					if (_comment.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCommentIdChanging(value);
					this.SendPropertyChanging();
					this._commentID = value;
					this.SendPropertyChanged("CommentId");
					this.OnCommentIdChanged();
				}
			}
		}
		
		[Column(Storage="_pictureID", Name="PictureId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int PictureId
		{
			get
			{
				return this._pictureID;
			}
			set
			{
				if ((_pictureID != value))
				{
					if (_picture.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPictureIdChanging(value);
					this.SendPropertyChanging();
					this._pictureID = value;
					this.SendPropertyChanged("PictureId");
					this.OnPictureIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_comment", OtherKey="Id", ThisKey="CommentId", Name="fk_Picture_has_Comment_Comment1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Comment Comment
		{
			get
			{
				return this._comment.Entity;
			}
			set
			{
				if (((this._comment.Entity == value) 
							== false))
				{
					if ((this._comment.Entity != null))
					{
						Comment previousComment = this._comment.Entity;
						this._comment.Entity = null;
						previousComment.PictureComments.Remove(this);
					}
					this._comment.Entity = value;
					if ((value != null))
					{
						value.PictureComments.Add(this);
						_commentID = value.Id;
					}
					else
					{
						_commentID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_picture", OtherKey="Id", ThisKey="PictureId", Name="fk_Picture_has_Comment_Picture1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Picture Picture
		{
			get
			{
				return this._picture.Entity;
			}
			set
			{
				if (((this._picture.Entity == value) 
							== false))
				{
					if ((this._picture.Entity != null))
					{
						Picture previousPicture = this._picture.Entity;
						this._picture.Entity = null;
						previousPicture.PictureComments.Remove(this);
					}
					this._picture.Entity = value;
					if ((value != null))
					{
						value.PictureComments.Add(this);
						_pictureID = value.Id;
					}
					else
					{
						_pictureID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="PictureVote")]
	public partial class PictureVote : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _pictureID;
		
		private int _voteID;
		
		private EntityRef<Picture> _picture = new EntityRef<Picture>();
		
		private EntityRef<Vote> _vote = new EntityRef<Vote>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnPictureIdChanged();
		
		partial void OnPictureIdChanging(int value);
		
		partial void OnVoteIdChanged();
		
		partial void OnVoteIdChanging(int value);
		#endregion
		
		
		public PictureVote()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_pictureID", Name="PictureId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int PictureId
		{
			get
			{
				return this._pictureID;
			}
			set
			{
				if ((_pictureID != value))
				{
					if (_picture.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPictureIdChanging(value);
					this.SendPropertyChanging();
					this._pictureID = value;
					this.SendPropertyChanged("PictureId");
					this.OnPictureIdChanged();
				}
			}
		}
		
		[Column(Storage="_voteID", Name="VoteId", DbType="int", IsPrimaryKey=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int VoteId
		{
			get
			{
				return this._voteID;
			}
			set
			{
				if ((_voteID != value))
				{
					if (_vote.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnVoteIdChanging(value);
					this.SendPropertyChanging();
					this._voteID = value;
					this.SendPropertyChanged("VoteId");
					this.OnVoteIdChanged();
				}
			}
		}
		
		#region Parents
		[Association(Storage="_picture", OtherKey="Id", ThisKey="PictureId", Name="fk_Picture_has_Vote_Picture1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Picture Picture
		{
			get
			{
				return this._picture.Entity;
			}
			set
			{
				if (((this._picture.Entity == value) 
							== false))
				{
					if ((this._picture.Entity != null))
					{
						Picture previousPicture = this._picture.Entity;
						this._picture.Entity = null;
						previousPicture.PictureVotes.Remove(this);
					}
					this._picture.Entity = value;
					if ((value != null))
					{
						value.PictureVotes.Add(this);
						_pictureID = value.Id;
					}
					else
					{
						_pictureID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_vote", OtherKey="Id", ThisKey="VoteId", Name="fk_Picture_has_Vote_Vote1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Vote Vote
		{
			get
			{
				return this._vote.Entity;
			}
			set
			{
				if (((this._vote.Entity == value) 
							== false))
				{
					if ((this._vote.Entity != null))
					{
						Vote previousVote = this._vote.Entity;
						this._vote.Entity = null;
						previousVote.PictureVotes.Remove(this);
					}
					this._vote.Entity = value;
					if ((value != null))
					{
						value.PictureVotes.Add(this);
						_voteID = value.Id;
					}
					else
					{
						_voteID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="User")]
	public partial class User : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _adminNote;
		
		private int _createEventID;
		
		private string _emailAddress;
		
		private System.Nullable<System.Guid> _guid;
		
		private int _id;
		
		private bool _isAdmin;
		
		private bool _isEmailAddressVerified;
		
		private bool _isLocked;
		
		private System.DateTime _lastLogin;
		
		private string _name;
		
		private EntitySet<Caption> _captions;
		
		private EntitySet<Comment> _comments;
		
		private EntitySet<ExternalLogin> _externalLogins;
		
		private EntitySet<OAuthToken> _oaUthTokens;
		
		private EntitySet<Picture> _pictures;
		
		private EntitySet<Vote> _votes;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAdminNoteChanged();
		
		partial void OnAdminNoteChanging(string value);
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnEmailAddressChanged();
		
		partial void OnEmailAddressChanging(string value);
		
		partial void OnGuidChanged();
		
		partial void OnGuidChanging(System.Nullable<System.Guid> value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnIsAdminChanged();
		
		partial void OnIsAdminChanging(bool value);
		
		partial void OnIsEmailAddressVerifiedChanged();
		
		partial void OnIsEmailAddressVerifiedChanging(bool value);
		
		partial void OnIsLockedChanged();
		
		partial void OnIsLockedChanging(bool value);
		
		partial void OnLastLoginChanged();
		
		partial void OnLastLoginChanging(System.DateTime value);
		
		partial void OnNameChanged();
		
		partial void OnNameChanging(string value);
		#endregion
		
		
		public User()
		{
			_captions = new EntitySet<Caption>(new Action<Caption>(this.Captions_Attach), new Action<Caption>(this.Captions_Detach));
			_comments = new EntitySet<Comment>(new Action<Comment>(this.Comments_Attach), new Action<Comment>(this.Comments_Detach));
			_externalLogins = new EntitySet<ExternalLogin>(new Action<ExternalLogin>(this.ExternalLogins_Attach), new Action<ExternalLogin>(this.ExternalLogins_Detach));
			_oaUthTokens = new EntitySet<OAuthToken>(new Action<OAuthToken>(this.OAuthTokens_Attach), new Action<OAuthToken>(this.OAuthTokens_Detach));
			_pictures = new EntitySet<Picture>(new Action<Picture>(this.Pictures_Attach), new Action<Picture>(this.Pictures_Detach));
			_votes = new EntitySet<Vote>(new Action<Vote>(this.Votes_Attach), new Action<Vote>(this.Votes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_adminNote", Name="AdminNote", DbType="varchar(140)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdminNote
		{
			get
			{
				return this._adminNote;
			}
			set
			{
				if (((_adminNote == value) 
							== false))
				{
					this.OnAdminNoteChanging(value);
					this.SendPropertyChanging();
					this._adminNote = value;
					this.SendPropertyChanged("AdminNote");
					this.OnAdminNoteChanged();
				}
			}
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_emailAddress", Name="EmailAddress", DbType="varchar(100)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string EmailAddress
		{
			get
			{
				return this._emailAddress;
			}
			set
			{
				if (((_emailAddress == value) 
							== false))
				{
					this.OnEmailAddressChanging(value);
					this.SendPropertyChanging();
					this._emailAddress = value;
					this.SendPropertyChanged("EmailAddress");
					this.OnEmailAddressChanged();
				}
			}
		}
		
		[Column(Storage="_guid", Name="Guid", DbType="char(36)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.Guid> Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				if ((_guid != value))
				{
					this.OnGuidChanging(value);
					this.SendPropertyChanging();
					this._guid = value;
					this.SendPropertyChanged("Guid");
					this.OnGuidChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_isAdmin", Name="IsAdmin", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsAdmin
		{
			get
			{
				return this._isAdmin;
			}
			set
			{
				if ((_isAdmin != value))
				{
					this.OnIsAdminChanging(value);
					this.SendPropertyChanging();
					this._isAdmin = value;
					this.SendPropertyChanged("IsAdmin");
					this.OnIsAdminChanged();
				}
			}
		}
		
		[Column(Storage="_isEmailAddressVerified", Name="IsEmailAddressVerified", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsEmailAddressVerified
		{
			get
			{
				return this._isEmailAddressVerified;
			}
			set
			{
				if ((_isEmailAddressVerified != value))
				{
					this.OnIsEmailAddressVerifiedChanging(value);
					this.SendPropertyChanging();
					this._isEmailAddressVerified = value;
					this.SendPropertyChanged("IsEmailAddressVerified");
					this.OnIsEmailAddressVerifiedChanged();
				}
			}
		}
		
		[Column(Storage="_isLocked", Name="IsLocked", DbType="tinyint(1)", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if ((_isLocked != value))
				{
					this.OnIsLockedChanging(value);
					this.SendPropertyChanging();
					this._isLocked = value;
					this.SendPropertyChanged("IsLocked");
					this.OnIsLockedChanged();
				}
			}
		}
		
		[Column(Storage="_lastLogin", Name="LastLogin", DbType="datetime", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public System.DateTime LastLogin
		{
			get
			{
				return this._lastLogin;
			}
			set
			{
				if ((_lastLogin != value))
				{
					this.OnLastLoginChanging(value);
					this.SendPropertyChanging();
					this._lastLogin = value;
					this.SendPropertyChanged("LastLogin");
					this.OnLastLoginChanged();
				}
			}
		}
		
		[Column(Storage="_name", Name="Name", DbType="varchar(45)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (((_name == value) 
							== false))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_captions", OtherKey="UserId", ThisKey="Id", Name="fk_Caption_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<Caption> Captions
		{
			get
			{
				return this._captions;
			}
			set
			{
				this._captions = value;
			}
		}
		
		[Association(Storage="_comments", OtherKey="UserId", ThisKey="Id", Name="fk_Comment_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<Comment> Comments
		{
			get
			{
				return this._comments;
			}
			set
			{
				this._comments = value;
			}
		}
		
		[Association(Storage="_externalLogins", OtherKey="UserId", ThisKey="Id", Name="fk_ExternalLogin_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<ExternalLogin> ExternalLogins
		{
			get
			{
				return this._externalLogins;
			}
			set
			{
				this._externalLogins = value;
			}
		}
		
		[Association(Storage="_oaUthTokens", OtherKey="UserId", ThisKey="Id", Name="fk_OauthTokens_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<OAuthToken> OAuthTokens
		{
			get
			{
				return this._oaUthTokens;
			}
			set
			{
				this._oaUthTokens = value;
			}
		}
		
		[Association(Storage="_pictures", OtherKey="UserId", ThisKey="Id", Name="fk_Picture_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<Picture> Pictures
		{
			get
			{
				return this._pictures;
			}
			set
			{
				this._pictures = value;
			}
		}
		
		[Association(Storage="_votes", OtherKey="UserId", ThisKey="Id", Name="fk_Vote_User1")]
		[DebuggerNonUserCode()]
		public EntitySet<Vote> Votes
		{
			get
			{
				return this._votes;
			}
			set
			{
				this._votes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_User_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.Users.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void Captions_Attach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void Captions_Detach(Caption entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void Comments_Attach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void Comments_Detach(Comment entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void ExternalLogins_Attach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void ExternalLogins_Detach(ExternalLogin entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void OAuthTokens_Attach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void OAuthTokens_Detach(OAuthToken entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void Pictures_Attach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void Pictures_Detach(Picture entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void Votes_Attach(Vote entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void Votes_Detach(Vote entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		#endregion
	}
	
	[Table(Name="Vote")]
	public partial class Vote : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _adminNote;
		
		private int _createEventID;
		
		private int _id;
		
		private int _userID;
		
		private int _weight;
		
		private EntitySet<PictureVote> _pictureVotes;
		
		private EntitySet<CaptionVote> _captionVotes;
		
		private EntityRef<Event> _event = new EntityRef<Event>();
		
		private EntityRef<User> _user = new EntityRef<User>();
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAdminNoteChanged();
		
		partial void OnAdminNoteChanging(string value);
		
		partial void OnCreateEventIdChanged();
		
		partial void OnCreateEventIdChanging(int value);
		
		partial void OnIdChanged();
		
		partial void OnIdChanging(int value);
		
		partial void OnUserIdChanged();
		
		partial void OnUserIdChanging(int value);
		
		partial void OnWeightChanged();
		
		partial void OnWeightChanging(int value);
		#endregion
		
		
		public Vote()
		{
			_pictureVotes = new EntitySet<PictureVote>(new Action<PictureVote>(this.PictureVotes_Attach), new Action<PictureVote>(this.PictureVotes_Detach));
			_captionVotes = new EntitySet<CaptionVote>(new Action<CaptionVote>(this.CaptionVotes_Attach), new Action<CaptionVote>(this.CaptionVotes_Detach));
			this.OnCreated();
		}
		
		[Column(Storage="_adminNote", Name="AdminNote", DbType="varchar(140)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AdminNote
		{
			get
			{
				return this._adminNote;
			}
			set
			{
				if (((_adminNote == value) 
							== false))
				{
					this.OnAdminNoteChanging(value);
					this.SendPropertyChanging();
					this._adminNote = value;
					this.SendPropertyChanged("AdminNote");
					this.OnAdminNoteChanged();
				}
			}
		}
		
		[Column(Storage="_createEventID", Name="CreateEventId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int CreateEventId
		{
			get
			{
				return this._createEventID;
			}
			set
			{
				if ((_createEventID != value))
				{
					if (_event.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCreateEventIdChanging(value);
					this.SendPropertyChanging();
					this._createEventID = value;
					this.SendPropertyChanged("CreateEventId");
					this.OnCreateEventIdChanged();
				}
			}
		}
		
		[Column(Storage="_id", Name="Id", DbType="int", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((_id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[Column(Storage="_userID", Name="UserId", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UserId
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((_userID != value))
				{
					if (_user.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}
		
		[Column(Storage="_weight", Name="Weight", DbType="int", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				if ((_weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		#region Children
		[Association(Storage="_pictureVotes", OtherKey="VoteId", ThisKey="Id", Name="fk_Picture_has_Vote_Vote1")]
		[DebuggerNonUserCode()]
		public EntitySet<PictureVote> PictureVotes
		{
			get
			{
				return this._pictureVotes;
			}
			set
			{
				this._pictureVotes = value;
			}
		}
		
		[Association(Storage="_captionVotes", OtherKey="VoteId", ThisKey="Id", Name="fk_Vote_has_Caption_Vote1")]
		[DebuggerNonUserCode()]
		public EntitySet<CaptionVote> CaptionVotes
		{
			get
			{
				return this._captionVotes;
			}
			set
			{
				this._captionVotes = value;
			}
		}
		#endregion
		
		#region Parents
		[Association(Storage="_event", OtherKey="Id", ThisKey="CreateEventId", Name="fk_Vote_Event1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public Event Event
		{
			get
			{
				return this._event.Entity;
			}
			set
			{
				if (((this._event.Entity == value) 
							== false))
				{
					if ((this._event.Entity != null))
					{
						Event previousEvent = this._event.Entity;
						this._event.Entity = null;
						previousEvent.Votes.Remove(this);
					}
					this._event.Entity = value;
					if ((value != null))
					{
						value.Votes.Add(this);
						_createEventID = value.Id;
					}
					else
					{
						_createEventID = default(int);
					}
				}
			}
		}
		
		[Association(Storage="_user", OtherKey="Id", ThisKey="UserId", Name="fk_Vote_User1", IsForeignKey=true)]
		[DebuggerNonUserCode()]
		public User User
		{
			get
			{
				return this._user.Entity;
			}
			set
			{
				if (((this._user.Entity == value) 
							== false))
				{
					if ((this._user.Entity != null))
					{
						User previousUser = this._user.Entity;
						this._user.Entity = null;
						previousUser.Votes.Remove(this);
					}
					this._user.Entity = value;
					if ((value != null))
					{
						value.Votes.Add(this);
						_userID = value.Id;
					}
					else
					{
						_userID = default(int);
					}
				}
			}
		}
		#endregion
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region Attachment handlers
		private void PictureVotes_Attach(PictureVote entity)
		{
			this.SendPropertyChanging();
			entity.Vote = this;
		}
		
		private void PictureVotes_Detach(PictureVote entity)
		{
			this.SendPropertyChanging();
			entity.Vote = null;
		}
		
		private void CaptionVotes_Attach(CaptionVote entity)
		{
			this.SendPropertyChanging();
			entity.Vote = this;
		}
		
		private void CaptionVotes_Detach(CaptionVote entity)
		{
			this.SendPropertyChanging();
			entity.Vote = null;
		}
		#endregion
	}
}
