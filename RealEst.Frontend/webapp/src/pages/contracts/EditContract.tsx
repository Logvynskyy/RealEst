import { useEffect, useState, useRef } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Contract } from "../../interfaces/Contract";
import { Endpoints } from "../../constants/endpoints";

const EditContract = () => {
  const { contractId } = useParams<{ contractId: string }>();
  const [contract, setContracts] = useState<Contract>();
  const navigate = useNavigate();

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

      setContracts(responseData);
    } catch (error) {
      console.error("Error getting contract:", error);
      return [];
    }
  };

  useEffect(() => {
    getContractById();
  }, []);

  const nameRef = useRef<HTMLInputElement>(null);
  const ibanRef = useRef<HTMLInputElement>(null);
  const priceRef = useRef<HTMLInputElement>(null);
  const rentToRef = useRef<HTMLInputElement>(null);

  const handleSubmit = async () => {
    try {
      const name = nameRef.current?.value;
      const iban = ibanRef.current?.value;
      const price = priceRef.current?.value;
      const rentTo = rentToRef.current?.value;

      const response = await fetch(
        "https://localhost:7115/api/Contracts/edit/" + contractId,
        {
          method: "PATCH",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
          body: JSON.stringify({
            name,
            iban,
            price,
            rentTo
          }),
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      try {
        navigate(Endpoints.contracts);
      } catch (error) {
        console.error("Redirect error:", error);
      }
    } catch (error) {
      console.error("Error patching the contract:", error);
    }
  };

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4">
        <h1 className="text-2xl">Назва</h1>
          <input
            type="text"
            name="name"
            placeholder="Enter name of the Contract"
            required
            ref={nameRef}
            defaultValue={contract?.name}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Об'єкт - {contract?.unit}</h1>
          <h1 className="text-2xl">Банківський акаунт</h1>
          <input
            type="text"
            name="iban"
            placeholder="Enter iban of the Bank account"
            required
            ref={ibanRef}
            defaultValue={contract?.iban}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Орендар - {contract?.tennant}</h1>
          <h1 className="text-2xl">Ціна</h1>
          <input
            type="number"
            name="price"
            placeholder="Enter price of the Contract"
            required
            ref={priceRef}
            defaultValue={contract?.price}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Дата початку оренди - {contract?.rentFrom ? contract.rentFrom.toLocaleDateString() : "Invalid Date"}</h1>
          <h1 className="text-2xl">Попередня дата закінчення оренди - {contract?.rentTo ? contract.rentTo.toLocaleDateString() : "Invalid Date"}</h1>
          <h1 className="text-2xl">Дата закінчення оренди</h1>
          <input
            type="date"
            name="rentTo"
            placeholder="Enter rent-end date"
            required
            ref={rentToRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <button
            type="button"
            onClick={handleSubmit}
            className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Редагувати
          </button>
        </form>
      </div>
    </div>
  );
};

export default EditContract;
