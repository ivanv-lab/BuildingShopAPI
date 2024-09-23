namespace BuildingShopAPI.Mappings
{
    public interface IMapper<Model,Dto,CreateDto>
    {
        public Model Map(Dto dto);
        public Dto Map(Model model);
        public Model Map(CreateDto createDto);
        public IEnumerable<Dto> MapList(
            IEnumerable<Model> models);
        public Model UpdateMap(Model model,
            CreateDto createdto);
    }
}
