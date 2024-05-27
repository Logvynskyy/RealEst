import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../../../@/components/ui/select";
import { Endpoints } from "../../constants/endpoints";

const CreateContact = () => {
  const contactTypes = [
    { id: 0, name: "ФОП" },
    { id: 1, name: "Фірма" },
    { id: 2, name: "Сайт з аренди нерухомості" }
  ];

  const [contactType, setContactType] = useState<number>(0);
  const navigate = useNavigate();
  const nameRef = useRef<HTMLInputElement>(null);
  const lastNameRef = useRef<HTMLInputElement>(null);
  const emailRef = useRef<HTMLInputElement>(null);
  const phoneNumberRef = useRef<HTMLInputElement>(null);
  const priorityRef = useRef<HTMLInputElement>(null);

  const handleSubmit = async () => {
    try {
      const name = nameRef.current?.value;
      const lastName = lastNameRef.current?.value;
      const email = emailRef.current?.value;
      const phoneNumber = phoneNumberRef.current?.value;
      const priority = priorityRef.current?.value;

      const response = await fetch("https://localhost:7115/api/Contacts/create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
        body: JSON.stringify({ id: 0, name, lastName, email, phoneNumber, contactType, priority }),
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      try {
        navigate(Endpoints.contacts);
      } catch (error) {
        console.error("Redirect error:", error);
      }
    } catch (error) {
      console.error("Creating error:", error);
    }
  };

  return (
    <div className="w-full flex min-h-screen justify-center items-center bg-[#6DDCFF]">
      <div className="bg-white p-8 rounded-lg shadow-md w-[30rem]">
        <form className="flex flex-col gap-4">
          <h1 className="text-2xl">Ім'я</h1>
          <input
            type="text"
            name="name"
            placeholder="Enter name of the Contact"
            required
            ref={nameRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Прізвище</h1>
          <input
            type="text"
            name="surname"
            placeholder="Enter surname of the Contact"
            required
            ref={lastNameRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Електронна пошта</h1>
          <input
            type="text"
            name="email"
            placeholder="Enter email of the Contact"
            required
            ref={emailRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Номер телефону</h1>
          <input
            type="text"
            name="phoneNumber"
            placeholder="Enter phone number of the Contact"
            required
            ref={phoneNumberRef}
            className="w-full rounded-md border border-gray-300 px-3 py-2 focus:outline-none focus:ring-1 focus:ring-blue-500 focus:border-blue-500"
          />
          <h1 className="text-2xl">Тип контакту</h1>
          <Select
            onValueChange={(value) => setContactType(Number.parseInt(value))}
          >
            <SelectTrigger className="w-full">
              <SelectValue placeholder={"Тип контакту"} />
            </SelectTrigger>
            <SelectContent>
              {contactTypes.map((type, index) => (
                <SelectItem key={index} value={type.id.toString()}>
                  {type.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          <h1 className="text-2xl">Пріоритет</h1>
          <input
            type="number"
            name="priority"
            placeholder="Enter priority of the Contact"
            required
            ref={priorityRef}
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

export default CreateContact;
