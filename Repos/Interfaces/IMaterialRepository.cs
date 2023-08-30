using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IMaterialRepository
    {

        public int GetMaterialNumber();



        public Material GetbyMaterialbyID(int materialID);



        public List<Material> GetMaterial();


        public void UpdateMaterial(int materialID, Material material);


        public void DeleteMaterial(int materialID);


        public void CreateMaterial(Material material);
        public int GetMaterialNumber(int courseID, int crsDayID);


        public void RemoveInstructor(int instructorID);
    }
}
