import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Tennant } from "../../interfaces/Tennant";
import { Endpoints } from "../../constants/endpoints";
import { TrashIcon } from "lucide-react";

const Debtors = () => {
  const [tennants, setTennants] = useState<Tennant[]>();
  const [debtors, setDebtors] = useState<Tennant[]>();

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

      setTennants(_prevTennants => responseData);
    } catch (error) {
      console.error("Error getting tennants:", error);
      return [];
    }
  };

  const getDebtors = async () => {
    setDebtors(tennants?.filter(tennant => tennant.isDebtor));
  };

  useEffect(() => {
    getTennants();
  }, [])

  useEffect(() => {
    getDebtors();
  }, [tennants]);

  const deleteDebtor = async (id: number) => {
    setDebtors((debtors) => debtors?.filter((debtor) => debtor.id != id));

    try {
        const response = await fetch(
          "https://localhost:7115/api/Tennants/clear-debt/" + id,
          {
            method: "PATCH",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + localStorage.getItem("JWTToken"),
            },
          }
        );
  
        if (!response.ok) {
          throw new Error(`Error: ${response.statusText}`);
        }
      } catch (error) {
        console.error("Error deleting tennant:", error);
        return [];
      }
  };

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          {debtors?.map((debtor, index) => (
            <div
              key={index}
              className="flex items-center justify-between min-w-full
                        hover:bg-gray-500 bg-gray-400 py-2 px-4 rounded-md font-medium text-white text-3xl"
            >
              <Link to={`${Endpoints.tennant + "/" + debtor.id}`} className="w-full">
                <div className="flex items-center">
                  <div>{debtor.displayString}</div>
                </div>
              </Link>
              <div className="flex gap-2 items-center z-20">
                <div
                  className="bg-white z-20 cursor-pointer"
                  onClick={() => deleteDebtor(debtor.id)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Debtors;
