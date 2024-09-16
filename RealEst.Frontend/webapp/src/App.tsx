import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import CreateOrganisation from "./pages/CreateOrganisation";
import { Endpoints } from "./constants/endpoints";
import Sidebar from "./pages/Sidebar";
import CreateAdminUser from "./pages/users/CreateAdminUser";
import Units from "./pages/units/Units";
import EditUnit from "./pages/units/EditUnit";
import OpenUnit from "./pages/units/OpenUnit";
import CreateUnit from "./pages/units/CreateUnit";
import Contacts from "./pages/contacts/Contacts";
import CreateContact from "./pages/contacts/CreateContact";
import OpenContact from "./pages/contacts/OpenContact";
import EditContact from "./pages/contacts/EditContact";
import Tennants from "./pages/tennants/Tennants";
import CreateTennant from "./pages/tennants/CreateTennant";
import OpenTennant from "./pages/tennants/OpenTennant";
import EditTennant from "./pages/tennants/EditTennant";
import Contracts from "./pages/contracts/Contracts";
import CreateContract from "./pages/contracts/CreateContract";
import OpenContract from "./pages/contracts/OpenContract";
import EditContract from "./pages/contracts/EditContract";
import Debtors from "./pages/debtors/Debtors";
import Users from "./pages/users/Users";
import CreateUser from "./pages/users/CreateUser";
import Incomes from "./pages/income/Incomes";

function Root() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Sidebar />}>
          <Route path={Endpoints.units} element={<Units />} />
          <Route path={Endpoints.unit + "/:unitId"} element={<OpenUnit />} />
          <Route path={Endpoints.createUnit} element={<CreateUnit />} />
          <Route path={Endpoints.editUnit + "/:unitId"} element={<EditUnit />}
          />

          <Route path={Endpoints.contracts} element={<Contracts />} />
          <Route path={Endpoints.contract + "/:contractId"} element={<OpenContract />} />
          <Route path={Endpoints.createContract} element={<CreateContract />} />
          <Route path={Endpoints.editContract + "/:contractId"} element={<EditContract />}
          />

          <Route path={Endpoints.tennants} element={<Tennants />} />
          <Route path={Endpoints.tennant + "/:tennantId"} element={<OpenTennant />} />
          <Route path={Endpoints.createTennant} element={<CreateTennant />} />
          <Route path={Endpoints.editTennant + "/:tennantId"} element={<EditTennant />}
          />

          <Route path={Endpoints.income} element={<Incomes />} />

          <Route path={Endpoints.debtors} element={<Debtors />} />

          <Route path={Endpoints.contacts} element={<Contacts />} />
          <Route path={Endpoints.contact + "/:contactId"} element={<OpenContact />} />
          <Route path={Endpoints.createContact} element={<CreateContact />} />
          <Route path={Endpoints.editContact + "/:contactId"} element={<EditContact />}
          />

          <Route path={Endpoints.users} element={<Users />} />
          <Route path={Endpoints.createUser} element={<CreateUser />} />
        </Route>

        <Route index element={<Login />} />
        <Route
          path={Endpoints.createOrganisation}
          element={<CreateOrganisation />}
        />
        <Route path={Endpoints.createAdminUser} element={<CreateAdminUser />} />
      </Routes>
    </Router>
  );
}

export default Root;
