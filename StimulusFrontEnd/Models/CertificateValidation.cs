using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace StimulusFrontEnd.Models
{
    public class CertificateValidation
    {
        /*
         fonction : recoit une collection de certificat lors de l'initialisation et l'ajoute a un store custom(store = base de donne des certificats valider) 
                    et lorsquil recoit un certificat, regarde si le certificat est present dans le store custom
         variables : 
            trustedRoots : certificat envoyer a l'initialisation pour l'ajouter au store
            intermediates : je sais pas sorry ;-;
            roots : = a trustedRoots, sert dans le cas ou on utiliserais l'invocation parallele
            intermeds : meme chose que roots
            sender : le httprequestmessage recu
            serverCert : certificat recu du serveur
            chain : chaine d'identification du server. garde en memoire tout les composant utiliser par le serveur qui a envoyer le certificat
            errors : erreur presente dans la requete
         */
        public static RemoteCertificateValidationCallback CreateCustomRootRemoteValidator(X509Certificate2Collection trustedRoots,X509Certificate2Collection intermediates = null)
        {
            if(trustedRoots == null)
            {
                throw new ArgumentNullException(nameof(trustedRoots));
            }
            if(trustedRoots.Count == 0)
            {
                throw new ArgumentNullException("pas fourni",nameof(trustedRoots));
            }

            X509Certificate2Collection roots = new X509Certificate2Collection(trustedRoots);

            X509Certificate2Collection intermeds = null;

            if(intermediates != null)
            {
                intermeds = new X509Certificate2Collection(intermediates);
            }

            intermediates = null;
            trustedRoots = null;

            return (sender, serverCert, chain, errors) =>
            {
                if ((errors & ~SslPolicyErrors.RemoteCertificateChainErrors) != 0)
                {
                    return false;
                }

                for (int i = 1; i < chain.ChainElements.Count; i++)
                {
                    chain.ChainPolicy.ExtraStore.Add(chain.ChainElements[i].Certificate);
                }

                if (intermeds != null)
                {
                    chain.ChainPolicy.ExtraStore.AddRange(intermeds);
                }

                chain.ChainPolicy.CustomTrustStore.Clear();
                chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                chain.ChainPolicy.CustomTrustStore.AddRange(roots);
                return chain.Build((X509Certificate2)serverCert);
            };
        }

        /*
         fonction : retourne une fonction qui utilise la methode de validation
         */
        public static Func<HttpRequestMessage,X509Certificate2,X509Chain,SslPolicyErrors,bool> CreateCustomRootValidator(X509Certificate2Collection trustedRoots,X509Certificate2Collection intermediates = null)
        {
            RemoteCertificateValidationCallback callback = CreateCustomRootRemoteValidator(trustedRoots, intermediates);
            return (message, serverCert, chain, errors) => callback(null, serverCert, chain, errors);
        }
    }
}
