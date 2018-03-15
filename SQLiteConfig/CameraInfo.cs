using Chloe.Entity;
using System.Data;

namespace DataBaseConfig
{
    [TableAttribute("CameraInfo")]
    public class CameraInfo : CameraType
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        //[Sequence("USERS_AUTOID")]//For oracle
        public int Id { get; set; }
        /// <summary>
        /// 工位Id编号
        /// </summary>
        public int StationId { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 相机序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 曝光时间
        /// </summary>
        public int Exposure { get; set; }

        /// <summary>
        /// 增益
        /// </summary>
        public double Gain { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Use { get; set; }

    }
}
