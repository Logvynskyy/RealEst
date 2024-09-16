import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Contract } from "../../interfaces/Contract";
const OpenContract = () => {
  const { contractId } = useParams<{ contractId: string }>();
  const [contract, setContract] = useState<Contract>();

  const getContractById = async () => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Contracts/" + contractId,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);
      responseData.rentFrom = new Date(responseData.rentFrom);
      responseData.rentTo = new Date(responseData.rentTo);

      setContract(responseData);
    } catch (error) {
      console.error("Error getting contract:", error);
      return [];
    }
  };

  useEffect(() => {
    getContractById();
  }, []);

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <h1 className="text-2xl">Назва - {contract?.name}</h1>
        <h1 className="text-2xl">Об'єкт - {contract?.unit}</h1>
        <h1 className="text-2xl">Банківський акаунт - {contract?.iban}</h1>
        <h1 className="text-2xl">Орендар - {contract?.tennant}</h1>
        <h1 className="text-2xl">Ціна - {contract?.price}</h1>
        <h1 className="text-2xl">Дата початку оренди - {contract?.rentFrom ? contract.rentFrom.toLocaleDateString() : "Invalid Date"}</h1>
        <h1 className="text-2xl">Дата закінчення оренди - {contract?.rentTo ? contract.rentTo.toLocaleDateString() : "Invalid Date"}</h1>
      </div>
    </div>
  );
};

export default OpenContract;
