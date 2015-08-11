using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Lab.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeEnvironmentOwner
{
    class Program
    {
        static void Main(string[] args)
        {
            var tfsUri = new Uri(args[0]);
            var environmentUri = new Uri(args[1]);
            var newOwner = args[2];

            var updatePack = new LabEnvironmentUpdatePack();


            using (var tfsService = new TfsTeamProjectCollection(tfsUri))
            {
                var labService = tfsService.GetService<LabService>();

                var labEnvironment = labService.GetLabEnvironment(environmentUri);

                foreach (var labsystem in labEnvironment.LabSystems)
                {
                    updatePack.ListOfUpdateCommands.Add(new UpdateLabSystemCommand(labsystem) { LabSystemUri = labsystem.Uri, VMOwner = newOwner });
                }

                labService.UpdateLabEnvironment(environmentUri, updatePack);
            }
        }
    }
}
