Use DVLD_V1;

Insert Into ApplicationTypes(ApplicationTypeTitle ,ApplicationFees)
 VALUES
		('NewLocal Driving License Service', 15.00),
		('Renew Driving License Service',5.00),
		('Replacement for aLost Driving License',	10.00),
		('Replacement for a Damaged Driving License',	5.00),
		('Release Detained Driving Licsense',	15.00),
		('New International License', 50.00),
		('Retake Test', 5.00) ;

Insert Into ApplicationStatus(ApplicationStatusTitle)
	VALUES
		('New'),
		('Cancelled'),
		('Completed');

Insert Into LicenseIssueReasons (IssueReasonsTitle, IssueReasonsFees)
	Values 
	('FirstTime',50), 
	('Renew' ,40), 
	('DamagedReplacement',30),
	('LostReplacement',30);

Insert Into Countries(CountryName)
	VALUES		
('Afghanistan'),('Albania'),('Algeria'),('Andorra'),('Angola'),
('Antigua and Barbuda'),('Argentina'),('Armenia'),('Australia'),
('Austria'),('Azerbaijan'),('Bahamas'),('Bahrain'),('Bangladesh'),
('Barbados'),('Belarus'),('Belgium'),('Belize'),('Benin	'),
('Bhutan'),('Bolivia'),('Bosnia and Herzegovina'),('Botswana'),
('Brazil'),('Brunei'),('Bulgaria'),('Burkina Faso'),('Burundi'),
('Cabo Verde'),('Cambodia'),('Cameroon'),('Canada'),
('Central African Republic'),('Chad'),('Chile'),('China'),
('Colombia'),('Comoros'),('Congo (Congo-Brazzaville)'),
('Costa Rica'),('Croatia'),('Cuba'),('Cyprus'),('Czechia'),
('Denmark '),('Djibouti'),('Dominica'),('Dominican Republic'),
('Ecuador'),('Egypt'),('El Salvador'),('Equatorial Guinea'),
('Eritrea'),('Estonia'),('Eswatini'),('Ethiopia'),('Fiji'),
('Finland'),('France'),('Gabon'),('Gambia'),('Georgia'),
('Germany'),('Ghana	'),('Greece'),('Grenada'),('Guatemala'),
('Guinea'),('Guinea-Bissau'),('Guyana'),('Haiti	'),('Honduras'),
('Hungary'),('Iceland'),('India	'),('Indonesia'),('Iran'),
('Iraq'),('Ireland'),('Israel'),('Italy'),('Jamaica'),('Japan'),
('Jordan '),('Kazakhstan'),('Kenya  '),('Kiribati'),('Kuwait'),
('Kyrgyzstan'),('Laos'),('Latvia'),('Lebanon'),('Lesotho'),
('Liberia'),('Libya	 '),('Liechtenstein'),('Lithuania '),
('Luxembourg'),('Madagascar'),('Malawi'),('Malaysia'),('Maldives'),
('Mali'),('Malta'),('Marshall Islands'),('Mauritania '),
('Mauritius'),('Mexico'),('Micronesia'),('Moldova'),('Monaco'),
('Mongolia'),('Montenegro'),('Morocco'),('Mozambique'),('Myanmar'),
('Namibia'),('Nauru	'),('Nepal'),('Netherlands'),('New Zealand'),
('Nicaragua'),('Niger'),('Nigeria'),('North Korea'),('North Macedonia'),
('Norway'),('Oman'),('Pakistan'),('Palau'),('Panama'),
('Papua New Guinea'),('Paraguay'),('Peru'),('Philippines'),
('Poland'),('Portugal'),('Qatar'),('Romania'),('Russia'),('Rwanda'),
('Saint Kitts and Nevis'),('Saint Lucia'),('Saint Vincent and the Grenadines'),
('Samoa'),('San Marino'),('Sao Tome and Principe'),('Saudi Arabia'),
('Senegalv'),('Serbia'),('Seychelles'),('Sierra Leone'),('Singapore'),
('Slovakia'),('Slovenia'),('Solomon Islands'),('Somalia'),('South Africa'),
('South Korea '),('South Sudan'),('Spain'),('Sri Lanka'),('Sudan'),
('Suriname'),('Sweden'),('Switzerland'),('Syria'),('Taiwan'),
('Tajikistan'),('Tanzania'),('Thailand'),('Timor-Leste'),
('Togo'),('Tonga'),('Trinidad and Tobago'),('Tunisia'),('Turkey'),
('Turkmenistan'),('Tuvalu'),('Uganda'),('Ukraine'),('United Arab Emirates'),
('United Kingdom'),('United States'),('Uruguay'),('Uzbekistan'),
('Vanuatu'),('Vatican City'),('Venezuela'),('Vietnam'),
('Yemen	'),('Zambia'),('Zimbabwe') ;


Insert Into Genders(GenderName)
	VALUES
	('Male'),
	('Female') ;

Insert Into LicenseClasses
(LicenseClassName, ClassDescription, MinimumeAllowedAge, DefaultValidityLength, ClassFees)
	VALUES
('Class 1 - Small Motorcycle', 'It allows the driver to drive small motorcycles, It is suitable for motorcycles with small capacity and limited power.', 18, 5, 15.00),
('Class 2 - Heavy Motorcycle License', 'Heavy Motorcycle License (Large Motorcycle License)', 21, 5, 30.00),
('Class 3 - Ordinary driving license', 'Ordinary driving license (car licence)', 18, 10, 20.00),
('Class 4 - Commercial', 'Commercial driving license (taxi/limousine)', 21, 10, 200.00),
('Class 5 - Agricultural', 'Agricultural and work vehicles used in farming or construction, (tractors / tillage machinery)', 21, 10, 50.00),
('Class 6 - Small and medium bus', 'Small and medium bus license',	21,	10,	250.00),
('Class 7 - Truck and heavy vehicle', 'Truck and heavy vehicle license', 21, 10, 300.00)  ;

Insert Into TestTypes
(TestTypeTitle, TestTypeDescription, TestTypeFees)
	VALUES
('Vision Test ', 'This assesses the applicant''s visual acuity to ensure they have sufficient vision to drive safely.',10.00),
('Written (Theory) Test', 'This Test assesses the applicants Knowledge of traffic', 20.00),
('Practical (Street) Test', 'This test evaluates the applicant''s driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.', 30.00)
