
namespace Tarea_KatherineMurilloJimenez.Models
{
    public class cls_Perros
    {
        public int Id { get; set; }
        public string EstadoSalud { get; set; }
        public float Peso { get; set; }
        public string PaisOrigen { get; set; }
        public List<cls_Razas> Razas { get; set; } = new List<cls_Razas>();
    }
}
