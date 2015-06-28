using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameImporter.Dto
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Data
    {

        private string baseImgUrlField;

        private DataGame gameField;

        /// <remarks/>
        public string baseImgUrl
        {
            get
            {
                return this.baseImgUrlField;
            }
            set
            {
                this.baseImgUrlField = value;
            }
        }

        /// <remarks/>
        public DataGame Game
        {
            get
            {
                return this.gameField;
            }
            set
            {
                this.gameField = value;
            }
        }

        public byte[] DowloadedFrontImage { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGame
    {

        private int idField;

        private string gameTitleField;

        private int platformIdField;

        private string platformField;

        private string releaseDateField;

        private string overviewField;

        private string eSRBField;

        private DataGameGenres genresField;

        private string playersField;

        private string coopField;

        private string youtubeField;

        private string publisherField;

        private string developerField;

        private decimal ratingField;

        private DataGameSimilar similarField;

        private DataGameImages imagesField;

        /// <remarks/>
        public int id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string GameTitle
        {
            get
            {
                return this.gameTitleField;
            }
            set
            {
                this.gameTitleField = value;
            }
        }

        /// <remarks/>
        public int PlatformId
        {
            get
            {
                return this.platformIdField;
            }
            set
            {
                this.platformIdField = value;
            }
        }

        /// <remarks/>
        public string Platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
            }
        }

        /// <remarks/>
        public string ReleaseDate
        {
            get
            {
                return this.releaseDateField;
            }
            set
            {
                this.releaseDateField = value;
            }
        }

        /// <remarks/>
        public string Overview
        {
            get
            {
                return this.overviewField;
            }
            set
            {
                this.overviewField = value;
            }
        }

        /// <remarks/>
        public string ESRB
        {
            get
            {
                return this.eSRBField;
            }
            set
            {
                this.eSRBField = value;
            }
        }

        /// <remarks/>
        public DataGameGenres Genres
        {
            get
            {
                return this.genresField;
            }
            set
            {
                this.genresField = value;
            }
        }

        /// <remarks/>
        public string Players
        {
            get
            {
                return this.playersField;
            }
            set
            {
                this.playersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Co-op")]
        public string Coop
        {
            get
            {
                return this.coopField;
            }
            set
            {
                this.coopField = value;
            }
        }

        /// <remarks/>
        public string Youtube
        {
            get
            {
                return this.youtubeField;
            }
            set
            {
                this.youtubeField = value;
            }
        }

        /// <remarks/>
        public string Publisher
        {
            get
            {
                return this.publisherField;
            }
            set
            {
                this.publisherField = value;
            }
        }

        /// <remarks/>
        public string Developer
        {
            get
            {
                return this.developerField;
            }
            set
            {
                this.developerField = value;
            }
        }

        /// <remarks/>
        public decimal Rating
        {
            get
            {
                return this.ratingField;
            }
            set
            {
                this.ratingField = value;
            }
        }

        /// <remarks/>
        public DataGameSimilar Similar
        {
            get
            {
                return this.similarField;
            }
            set
            {
                this.similarField = value;
            }
        }

        /// <remarks/>
        public DataGameImages Images
        {
            get
            {
                return this.imagesField;
            }
            set
            {
                this.imagesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameGenres
    {

        private string genreField;

        /// <remarks/>
        public string genre
        {
            get
            {
                return this.genreField;
            }
            set
            {
                this.genreField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameSimilar
    {

        private int similarCountField;

        private DataGameSimilarGame[] gameField;

        /// <remarks/>
        public int SimilarCount
        {
            get
            {
                return this.similarCountField;
            }
            set
            {
                this.similarCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Game")]
        public DataGameSimilarGame[] Game
        {
            get
            {
                return this.gameField;
            }
            set
            {
                this.gameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameSimilarGame
    {

        private int idField;

        private int platformIdField;

        /// <remarks/>
        public int id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public int PlatformId
        {
            get
            {
                return this.platformIdField;
            }
            set
            {
                this.platformIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImages
    {

        private DataGameImagesFanart[] fanartField;

        private DataGameImagesBoxart[] boxartField;

        private DataGameImagesBanner[] bannerField;

        private DataGameImagesScreenshot screenshotField;

        private DataGameImagesClearlogo clearlogoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fanart")]
        public DataGameImagesFanart[] fanart
        {
            get
            {
                return this.fanartField;
            }
            set
            {
                this.fanartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("boxart")]
        public DataGameImagesBoxart[] boxart
        {
            get
            {
                return this.boxartField;
            }
            set
            {
                this.boxartField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("banner")]
        public DataGameImagesBanner[] banner
        {
            get
            {
                return this.bannerField;
            }
            set
            {
                this.bannerField = value;
            }
        }

        /// <remarks/>
        public DataGameImagesScreenshot screenshot
        {
            get
            {
                return this.screenshotField;
            }
            set
            {
                this.screenshotField = value;
            }
        }

        /// <remarks/>
        public DataGameImagesClearlogo clearlogo
        {
            get
            {
                return this.clearlogoField;
            }
            set
            {
                this.clearlogoField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesFanart
    {

        private DataGameImagesFanartOriginal originalField;

        private string thumbField;

        /// <remarks/>
        public DataGameImagesFanartOriginal original
        {
            get
            {
                return this.originalField;
            }
            set
            {
                this.originalField = value;
            }
        }

        /// <remarks/>
        public string thumb
        {
            get
            {
                return this.thumbField;
            }
            set
            {
                this.thumbField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesFanartOriginal
    {

        private int widthField;

        private int heightField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesBoxart
    {

        private string sideField;

        private int widthField;

        private int heightField;

        private string thumbField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string side
        {
            get
            {
                return this.sideField;
            }
            set
            {
                this.sideField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string thumb
        {
            get
            {
                return this.thumbField;
            }
            set
            {
                this.thumbField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesBanner
    {

        private int widthField;

        private int heightField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesScreenshot
    {

        private DataGameImagesScreenshotOriginal originalField;

        private string thumbField;

        /// <remarks/>
        public DataGameImagesScreenshotOriginal original
        {
            get
            {
                return this.originalField;
            }
            set
            {
                this.originalField = value;
            }
        }

        /// <remarks/>
        public string thumb
        {
            get
            {
                return this.thumbField;
            }
            set
            {
                this.thumbField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesScreenshotOriginal
    {

        private int widthField;

        private int heightField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DataGameImagesClearlogo
    {

        private int widthField;

        private int heightField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


}
