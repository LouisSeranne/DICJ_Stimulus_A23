texte = ''
onglets = False

# Si il y a des onglets
if (onglets == True):
	texte = 'Choisissez un onglet pour commencer a coder !'
# Si il n'y a pas d'onglets
elif (onglets == False):
	texte = 'Creez un onglet avec le bouton '+' puis commencez a coder !'
print (texte)