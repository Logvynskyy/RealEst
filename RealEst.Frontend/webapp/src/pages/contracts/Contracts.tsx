import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Contract } from "../../interfaces/Contract";
import { Endpoints } from "../../constants/endpoints";
import { PencilIcon, TrashIcon } from "lucide-react";

const Contracts = () => {
  const [contracts, setContracts] = useState<Contract[]>();
  const navigate = useNavigate();

  const getContracts = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Contracts", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      setContracts(responseData);
    } catch (error) {
      console.error("Error getting contracts:", error);
      return [];
    }
  };

  useEffect(() => {
    getContracts();
  }, []);

  const createContract = async () => {
    try {
      navigate(Endpoints.createContract);
    } catch (error) {
      console.error("Redirect error:", error);
    }
  };

  const deleteContract = async (id: number) => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Contracts/delete/" + id,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      setContracts((contracts) => contracts?.filter((contract) => contract.id != id));
    } catch (error) {
      console.error("Error deleting contract:", error);
      return [];
    }
  };

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          {contracts?.map((contract, index) => (
            <div
              key={index}
              className="flex items-center justify-between min-w-full
                        hover:bg-gray-500 bg-gray-400 py-2 px-4 rounded-md font-medium text-white text-3xl"
            >
              <Link to={`${Endpoints.contract + "/" + contract.id}`} className="w-full">
                <div className="flex items-center">
                  <div>{contract.displayString}</div>
                </div>
              </Link>
              <div className="flex gap-2 items-center z-20">
                <Link
                  to={`${Endpoints.editContract + "/" + contract.id}`}
                  className="bg-white"
                >
                  <PencilIcon className="stroke-black" />
                </Link>
                <div
                  className="bg-white z-20 cursor-pointer"
                  onClick={() => deleteContract(contract.id)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
              </div>
            </div>
          ))}
        </div>
        <button
          onClick={createContract}
          className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 
                    focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                    absolute bottom-5 right-5 p-4 text-2xl"
        >
          Додати контракт
        </button>
      </div>
    </div>
  );
};

export default Contracts;
