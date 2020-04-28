def OnGenerateInfill():
	infills = list()
	for shape in shapes:
		infill = list()
		infill.append(((0, 25), (50, 50)))
		infill.append(((50, 0), (100, 100)))
	infills.append(infill)
	return infills
