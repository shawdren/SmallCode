using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsServiceTemplate
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {

        string serviceName = "ServiceName";
        string serviceDesc = "ServiceDesc";
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller processInstaller;
        public ProjectInstaller()
        {
            // define and create the service installer
            serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = serviceName;
            serviceInstaller.DisplayName = serviceDesc;

            Installers.Add(serviceInstaller);

            // define and create the process installer
            processInstaller = new ServiceProcessInstaller();
            //#if RUNUNDERSYSTEM
            processInstaller.Account = ServiceAccount.LocalSystem;
            //#else
            // should prompt for user on install
            //processInstaller.Account = ServiceAccount.User;
            //processInstaller.Username = null;
            //processInstaller.Password = null;
            //#endif
            Installers.Add(processInstaller);

            InitializeComponent();
        }

        public override void Install(IDictionary stateServer)
        {
            Microsoft.Win32.RegistryKey system,
                //HKEY_LOCAL_MACHINE\Services\CurrentControlSet
                currentControlSet,
                //...\Services
                services,
                //...\<Service Name>
                service;

            try
            {
                //Let the project installer do its job
                base.Install(stateServer);

                //Open the HKEY_LOCAL_MACHINE\SYSTEM key
                system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
                //Open CurrentControlSet
                currentControlSet = system.OpenSubKey("CurrentControlSet");
                //Go to the services key
                services = currentControlSet.OpenSubKey("Services");
                //Open the key for your service, and allow writing
                service = services.OpenSubKey(serviceInstaller.ServiceName, true);
                //Add your service's description as a REG_SZ value named "Description"
                service.SetValue("Description", "FrontRange Application Integration Framework Server.");
                //(Optional) Add some custom information your service will use...
                //config = service.CreateSubKey("Parameters");
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown during service installation:\n" + e.ToString());
            }
        }

        public override void Uninstall(IDictionary stateServer)
        {
            Microsoft.Win32.RegistryKey system,
                currentControlSet,
                services,
                service;

            try
            {
                //Drill down to the service key and open it with write permission
                system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
                currentControlSet = system.OpenSubKey("CurrentControlSet");
                services = currentControlSet.OpenSubKey("Services");
                service = services.OpenSubKey(serviceInstaller.ServiceName, true);
                //Delete any keys you created during installation (or that your service created)
                service.DeleteSubKeyTree("Parameters");
                //...
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception encountered while uninstalling service:\n" + e.ToString());
            }
            finally
            {
                //Let the project installer do its job
                base.Uninstall(stateServer);
            }
        }
    }
}
