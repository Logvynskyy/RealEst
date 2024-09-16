import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import { Endpoints } from "../../constants/endpoints";

const CreateAdminUser = () => {
  const organisationId = localStorage.getItem("OrganisationId");

  const usernameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);
  const navigate = useNavigate();

  const handleSubmit = async () => {
    const username = usernameRef.current?.value;
    const password = passwordRef.current?.value;

    try {
      const response = await fetch(
        "https://localhost:7115/api/Authentication/RegisterOrganisationOwner",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ username, password, organisationId }),
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      try {
        navigate(Endpoints.units);
      } catch (error) {
        console.error("Redirect error:", error);
      }
    } catch (error) {
      console.error("Creation error:", error);
    }
  };

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4">
          <input
            type="email"
            name="email"
            placeholder="Enter email"
            required
            ref={usernameRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <input
            type="password"
            name="password"
            placeholder="Enter password"
            required
            ref={passwordRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <button
            type="button"
            onClick={handleSubmit}
            className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Створити
          </button>
        </form>
      </div>
    </div>
  );
};

export default CreateAdminUser;
