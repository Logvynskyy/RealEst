import { Outlet, Link } from "react-router-dom";
import { Endpoints } from "../constants/endpoints";

interface NavItem {
  text: string;
  href: string;
}

const navigation: NavItem[] = [
  { text: "Об'єкти", href: Endpoints.units },
  { text: "Контракти", href: Endpoints.contracts },
  { text: "Орендарі", href: Endpoints.tennants },
  { text: "Прибутки", href: Endpoints.income },
  { text: "Боржники", href: Endpoints.debtors },
  { text: "Контакти", href: Endpoints.contacts },
  { text: "Користувачі", href: Endpoints.users },
];

const Sidebar: React.FC = () => {
  return (
    <div className="flex min-h-screen">
      <div className="w-1/5 bg-[#E7EDFF] border-r-4 border-gray-400">
        <div className="flex flex-col space-y-4 h-full justify-around p-2">
          {navigation
            .filter((item) => localStorage.getItem("IsAdmin") === "true" ? item : item.text !== "Користувачі")
            .map((item, index) => (
              <Link key={index} to={`${item.href}`}>
                <div
                  className="flex items-center justify-center h-20
                                hover:bg-blue-800 bg-blue-500 py-2 px-4 rounded-md font-medium text-white text-3xl"
                >
                  {item.text}
                </div>
              </Link>
            ))}
        </div>
      </div>
      <div className="flex-1 p-0">
        <Outlet />
      </div>
    </div>
  );
};

export default Sidebar;
