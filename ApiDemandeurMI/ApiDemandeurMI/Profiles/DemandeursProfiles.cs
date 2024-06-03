using ApiDemandeurMI.Dtos;
using ApiDemandeurMI.Models;
using AutoMapper;

namespace ApiDemandeurMI.Profiles
{
    public class DemandeursProfiles : Profile
    {
        public DemandeursProfiles()
        {
            CreateMap<Demandeur, DemandeursDto>();
            CreateMap<DemandeursDto, Demandeur>();
            CreateMap<Demandeur, DemandeursDtoin>();
            CreateMap<DemandeursDtoin, Demandeur>();
        }
    }
}
