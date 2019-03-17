using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace PixelStudio.Models
{
    internal abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal abstract class BaseCollection<T> : BaseModel
    {
        protected readonly BindingListEx<T> _Data;

        protected BaseCollection()
        {
            _Data = new BindingListEx<T>();
        }

        public event ListChangedEventHandler ListChanged
        {
            add => _Data.ListChanged += value;
            remove => _Data.ListChanged -= value;
        }

        public event EventHandler<ListItemChangingEventArgs<T>> ListItemChanging
        {
            add => _Data.ListItemChanging += value;
            remove => _Data.ListItemChanging -= value;
        }

        public event EventHandler<ListItemRemovingEventArgs<T>> ListItemRemoving
        {
            add => _Data.ListItemRemoving += value;
            remove => _Data.ListItemRemoving -= value;
        }
    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    [XmlRoot(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", ElementName = "Project")]
    internal class ProjectModel : BaseModel
    {
        private string _Name = "New Project";
        private ImageReferenceCollection _ImageReferences;
        private TimelineModel _Timeline;
        private bool _IsDirty;

        public ProjectModel()
        {
            ImageReferences = new ImageReferenceCollection();
            Timeline = new TimelineModel();
        }

        [XmlAttribute("Name")]
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        #region ImageReferences

        [XmlElement("ImageReferences")]
        public ImageReferenceCollection ImageReferences
        {
            get => _ImageReferences;
            set
            {
                if (_ImageReferences != value)
                {
                    if (_ImageReferences != null) _ImageReferences.ListChanged -= OnImageReferencesListChanged;
                    _ImageReferences = value;
                    if (_ImageReferences != null) _ImageReferences.ListChanged += OnImageReferencesListChanged;
                }
            }
        }

        private void OnImageReferencesListChanged(object sender, ListChangedEventArgs e)
        {
            IsDirty = true;
        }

        #endregion

        #region Timeline

        [XmlElement("Timeline")]
        public TimelineModel Timeline
        {
            get => _Timeline;
            set
            {
                if (_Timeline != value)
                {
                    if (_Timeline != null) _Timeline.ListChanged -= OnTimelineListChanged;
                    _Timeline = value;
                    if (_Timeline != null) _Timeline.ListChanged += OnTimelineListChanged;
                }
            }
        }

        private void OnTimelineListChanged(object sender, ListChangedEventArgs e)
        {
            IsDirty = true;
        }

        #endregion

        [XmlIgnore]
        public bool IsDirty
        {
            get => _IsDirty;
            set
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                    OnPropertyChanged(nameof(IsDirty));
                }
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            // Must call base directly to avoid reentrancy that would cause a StackOverflowException
            if (!_IsDirty)
            {
                _IsDirty = true;
                base.OnPropertyChanged(nameof(IsDirty));
            }
            base.OnPropertyChanged(propertyName);
        }
    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    internal class ImageReferenceCollection : BaseCollection<ImageReferenceModel>
    {
        [XmlElement("ImageReference")]
        public ImageReferenceModel[] ImageReferencesData
        {
            get => _Data.ToArray();
            set => _Data.SetItems(value);
        }

        [XmlIgnore]
        public IList<ImageReferenceModel> ImageReferences => _Data;

        public void Add(string filePath)
        {
            var id = (_Data.Count > 0 ? _Data.Max(irm => irm.ID) : 0) + 1;
            _Data.Add(new ImageReferenceModel { ID = id, FilePath = filePath });
        }
    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    internal class ImageReferenceModel : BaseModel
    {
        private bool _IsValid;

        [XmlAttribute("ID")]
        public long ID { get; set; }

        [XmlAttribute("FilePath")]
        public string FilePath { get; set; }
        
        [XmlIgnore]
        public bool IsValid
        {
            get => _IsValid;
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    internal enum Transition
    {
        [XmlEnum("none")]
        None,

        [XmlEnum("fade")]
        Fade,

        [XmlEnum("wipe_left")]
        WipeLeft,

        [XmlEnum("wipe_right")]
        WipeRight,

        [XmlEnum("wipe_up")]
        WipeUp,

        [XmlEnum("wipe_down")]
        WipeDown,

        [XmlEnum("dissolve")]
        Dissolve,

        [XmlEnum("print_stack")]
        PrintStack,
    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    internal class TimelineModel : BaseCollection<TimelineItemModel>
    {
        private int _Width;
        private int _Height;
        private float _FrameRate;
        private Color _BackgroundColor;

        public TimelineModel()
        {
            _Width = 1920;
            _Height = 1080;
            _FrameRate = 60.0f;
            _BackgroundColor = Color.Black;
        }

        [XmlElement("TimelineItem")]
        public TimelineItemModel[] TimelineItemsData
        {
            get => _Data.ToArray();
            set => _Data.SetItems(value);
        }

        [XmlIgnore]
        public IList<TimelineItemModel> TimelineItems => _Data;
        
        [XmlAttribute("Width")]
        [DefaultValue(1920)]
        public int Width
        {
            get => _Width;
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        [XmlAttribute("Height")]
        [DefaultValue(1080)]
        public int Height
        {
            get => _Height;
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        [XmlAttribute("FrameRate")]
        [DefaultValue(60.0f)]
        public float FrameRate
        {
            get => _FrameRate;
            set
            {
                if (_FrameRate != value)
                {
                    _FrameRate = value;
                    OnPropertyChanged(nameof(FrameRate));
                }
            }
        }

        [XmlAttribute("BackgroundColor")]
        [DefaultValue(0xFF000000)]
        public int BackgroundColorValue
        {
            get => BackgroundColor.ToArgb();
            set => BackgroundColor = Color.FromArgb(value);
        }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get => _BackgroundColor;
            set
            {
                if (_BackgroundColor != value)
                {
                    _BackgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        public TimeSpan TotalTime => TimeSpan.FromMilliseconds(TimelineItems.Any() ? TimelineItems.Sum(item => item.DurationMs) : 0);

        // TODO Additional informational properties
    }

    [Serializable]
    [XmlType(Namespace = "http://schneenet.com/PixelStudio/Project.xsd", AnonymousType = true)]
    internal class TimelineItemModel : BaseModel
    {
        private TimeSpan _Duration;
        private TimeSpan _TransitionDuration;
        private Transition _Transition;
        private float _Rotation;
        private bool _ShowBorder;
        private Color _BorderColor;
        private string _BorderText;
        private Color _BorderTextColor;
        private string _BorderTextFont;

        [XmlAttribute("ReferenceID")]
        public long ReferenceID { get; set; }

        [XmlAttribute("Duration")]
        public long DurationMs
        {
            get => (long)Duration.TotalMilliseconds;
            set => Duration = TimeSpan.FromMilliseconds(value);
        }
        
        [XmlIgnore]
        public TimeSpan Duration
        {
            get => _Duration;
            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        [XmlAttribute("TransitionDuration")]
        public long TransitionDurationMs
        {
            get => (long)TransitionDuration.TotalMilliseconds;
            set => TransitionDuration = TimeSpan.FromMilliseconds(value);
        }

        [XmlIgnore]
        public TimeSpan TransitionDuration
        {
            get => _TransitionDuration;
            set
            {
                if (_TransitionDuration != value)
                {
                    _TransitionDuration = value;
                    OnPropertyChanged(nameof(TransitionDuration));
                }
            }
        }

        [XmlAttribute("Transition")]
        public Transition Transition
        {
            get => _Transition;
            set
            {
                if (_Transition != value)
                {
                    _Transition = value;
                    OnPropertyChanged(nameof(Transition));
                }
            }
        }

        [XmlAttribute("Rotation")]
        [DefaultValue(0.0f)]
        public float Rotation
        {
            get => _Rotation;
            set
            {
                if (_Rotation != value)
                {
                    _Rotation = value;
                    OnPropertyChanged(nameof(Rotation));
                }
            }
        }

        [XmlAttribute("ShowBorder")]
        [DefaultValue(false)]
        public bool ShowBorder
        {
            get => _ShowBorder;
            set
            {
                if (_ShowBorder != value)
                {
                    _ShowBorder = value;
                    OnPropertyChanged(nameof(ShowBorder));
                }
            }
        }

        [XmlAttribute("BorderColor")]
        public int BorderColorValue
        {
            get => BorderColor.ToArgb();
            set => BorderColor = Color.FromArgb(value);
        }

        [XmlIgnore]
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                if (_BorderColor != value)
                {
                    _BorderColor = value;
                    OnPropertyChanged(nameof(BorderColor));
                }
            }
        }

        [XmlAttribute("BorderText")]
        [DefaultValue(null)]
        public string BorderText
        {
            get => _BorderText;
            set
            {
                if (_BorderText != value)
                {
                    _BorderText = value;
                    OnPropertyChanged(nameof(BorderText));
                }
            }
        }

        [XmlAttribute("BorderTextColor")]
        public int BorderTextColorValue
        {
            get => BorderTextColor.ToArgb();
            set => BorderTextColor = Color.FromArgb(value);
        }

        [XmlIgnore]
        public Color BorderTextColor
        {
            get => _BorderTextColor;
            set
            {
                if (_BorderTextColor != value)
                {
                    _BorderTextColor = value;
                    OnPropertyChanged(nameof(BorderTextColor));
                }
            }
        }

        [XmlAttribute("BorderTextFont")]
        [DefaultValue(null)]
        public string BorderTextFont
        {
            get => _BorderTextFont;
            set
            {
                if (_BorderTextFont != value)
                {
                    _BorderTextFont = value;
                    OnPropertyChanged(nameof(BorderTextFont));
                }
            }
        }

        // TODO Properties of a timeline item
    }
}

