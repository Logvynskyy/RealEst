import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Tennant } from "../../interfaces/Tennant";
import { Endpoints } from "../../constants/endpoints";
import { PencilIcon, TrashIcon } from "lucide-react";
import AddDebt from "../debtors/AddDebt";

const Tennants = () => {
  const [tennants, setTennants] = useState<Tennant[]>();
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();

  const getTennants = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Tennants", {
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

      setTennants(responseData);
    } catch (error) {
      console.error("Error getting tennants:", error);
      return [];
    }
  };

  useEffect(() => {
    getTennants();
  }, []);

  const createTennant = async () => {
    try {
      navigate(Endpoints.createTennant);
    } catch (error) {
      console.error("Redirect error:", error);
    }
  };

  const addDebt = async (data: {
    id: number,
    debt: number
  }) => {
    console.log("Submitted data:", data);

    try {
      const response = await fetch(
        "https://localhost:7115/api/Tennants/add-debt/" + data.id,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
          body: JSON.stringify(data.debt)
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }
    } catch (error) {
      console.error("Error setting debt to tennant:", error);
      return [];
    }

    setIsOpen(false);
  };

  const deleteTennant = async (id: number) => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Tennants/delete/" + id,
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

      setTennants((tennants) => tennants?.filter((tennant) => tennant.id != id));
    } catch (error) {
      console.error("Error deleting tennant:", error);
      return [];
    }
  };

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          {tennants?.map((tennant, index) => (
            <div
              key={index}
              className="flex items-center justify-between min-w-full
                        hover:bg-gray-500 bg-gray-400 py-2 px-4 rounded-md font-medium text-white text-3xl"
            >
              <Link to={`${Endpoints.tennant + "/" + tennant.id}`} className="w-full">
                <div className="flex items-center">
                  <div>{tennant.displayString}</div>
                </div>
              </Link>
              <div className="flex gap-2 items-center z-20">
                <Link
                  to={`${Endpoints.editTennant + "/" + tennant.id}`}
                  className="bg-white"
                >
                  <PencilIcon className="stroke-black" />
                </Link>
                <div
                  className="bg-white z-20 cursor-pointer"
                  onClick={() => deleteTennant(tennant.id)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
              </div>
            </div>
          ))}
        </div>
        <div>
        <AddDebt
            isOpen={isOpen}
            onClose={() => setIsOpen(false)}
            onSubmit={addDebt}
          />
        <button
          onClick={() => setIsOpen(true)}
          className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 
                    focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                    absolute bottom-5 right-15 p-4 text-2xl"
        >
          Додати борг
        </button>
        <button
          onClick={createTennant}
          className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 
                    focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                    absolute bottom-5 right-5 p-4 text-2xl"
        >
          Додати орендаря
        </button>
        </div>
      </div>
    </div>
  );
};

export default Tennants;
