using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Missoes
{
    public class Missao : Entity<MissaoId>, IAggregateRoot 
    {
        public int grauDificuldade {get; private set;}

        public bool estado {get; private set; }

        private Missao()
        {
            
        }

        public Missao(int grauDificuldade, bool estado)
        {
            this.grauDificuldade = grauDificuldade;
            this.estado = estado;
        }
    }
}