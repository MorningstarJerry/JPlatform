using Domain.Entities;
using Mapster;

namespace Application.Dto.Facility
{
    public class FileStoresCreateDto : IRegister
    {
        public string? name { get; set; }
        public string? fileType { get; set; }
        public string? extname { get; set; }
        public string? path { get; set; }
        public long? size { get; set; } = 0;
        public string? url { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FileStores, FileStoresCreateDto>()
            .Map(dest => dest.size,
                src => $"{long.Parse(src.size)}");
        }
    }
}
