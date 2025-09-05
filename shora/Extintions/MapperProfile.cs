using AutoMapper;
using DomianLayar.Entities;
using shora.Dtos.Cases;

namespace shora.Extintions
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            // Case → CaseDto (عرض تفصيلي)
            CreateMap<Case, GetCaseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FirstName : null));

            // Case → CaseListDto (عرض مختصر)
            CreateMap<Case, CaseListDto>();

            // CaseCreateDto → Case (إنشاء قضية جديدة)
            CreateMap<CaseCreateDto, Case>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")); // افتراضياً القضية جديدة

            // CaseUpdateDto → Case (تحديث قضية موجودة)
            CreateMap<CaseUpdateDto, Case>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // الشرط ده عشان ميكتبش null فوق القيم القديمة
        }
    }
    }

