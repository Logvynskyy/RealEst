import { useState, useEffect } from "react";
import Modal from 'react-modal';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../../@/components/ui/select";
import { Tennant } from "../../interfaces/Tennant";

interface PopupProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (data: any) => void;
  }

const AddDebt: React.FC<PopupProps> = ({ isOpen, onClose, onSubmit }) => {
  const [formData, setFormData] = useState<any>({});
  const [tennants, setTennants] = useState<Tennant[]>();
  const [id, setTennantId] = useState<number>(0);

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
  },[])

  useEffect(() => {
    setFormData((prevState: any) => ({ ...prevState, ["id"]: id }));
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prevState: any) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onSubmit(formData);   
  };

  return (
    <Modal isOpen={isOpen} onRequestClose={onClose} className="flex h-full justify-center items-center">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4" onSubmit={handleSubmit}>
        <h1 className="text-2xl">Орендар</h1>
          <Select onValueChange={(value) => setTennantId(Number.parseInt(value))}>
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Орендар"} />
            </SelectTrigger>
            <SelectContent>
              {tennants?.length && tennants.length > 0 && tennants.map((tennant, index) => (
                <SelectItem key={index} value={tennant.id.toString()}>
                  {tennant.displayString}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <h1 className="text-2xl">Борг</h1>
          <input
            type="number"
            name="debt"
            placeholder="Enter wanted amount"
            required
            onChange={handleChange}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <button
            type="submit"
            className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Додати
          </button>
        </form>
      </div>
    </Modal>
  );
};

export default AddDebt;
