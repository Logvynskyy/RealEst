import { useState, useEffect } from "react";
import Modal from 'react-modal';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../../../@/components/ui/select";

interface PopupProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (data: any) => void;
  }

const CreateDefect: React.FC<PopupProps> = ({ isOpen, onClose, onSubmit }) => {
  const defectTypes = [
    { id: 0, name: "Внутрішній косметичний" },
    { id: 1, name: "Внутрішній інфраструктурний" },
    { id: 2, name: "Зовнішній косметичний" },
    { id: 3, name: "Зовнішній інфраструктурний" }
  ];

  const [formData, setFormData] = useState<any>({});
  const [defectType, setDefectType] = useState<number>(0);
  const [id, setId] = useState<number>(0);

  useEffect(() => {
    setFormData((prevState: any) => ({ ...prevState, ["id"]: id }));
    setFormData((prevState: any) => ({ ...prevState, ["defectType"]: defectType }));
  }, [defectType]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prevState: any) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setId(id+1);
    onSubmit(formData);   
  };

  return (
    <Modal isOpen={isOpen} onRequestClose={onClose} className="flex h-full justify-center items-center">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4" onSubmit={handleSubmit}>
          <h1 className="text-2xl">Назва</h1>
          <input
            type="text"
            name="name"
            placeholder="Enter name of the Unit"
            required
            onChange={handleChange}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Опис</h1>
          <input
            type="text"
            name="description"
            placeholder="Enter address of the Unit"
            required
            onChange={handleChange}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Тип дефекту</h1>
          <Select
            onValueChange={(value) => setDefectType(Number.parseInt(value))}
          >
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Тип дефекту"} />
            </SelectTrigger>
            <SelectContent>
              {defectTypes.map((type, index) => (
                <SelectItem key={index} value={type.id.toString()}>
                  {type.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <button
            type="submit"
            className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
          >
            Створити
          </button>
        </form>
      </div>
    </Modal>
  );
};

export default CreateDefect;
